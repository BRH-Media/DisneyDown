using System;
using System.IO;
using System.Reflection;
using DisneyDown.Common.KeySystem.Schemas;
using DisneyDown.Common.Security;
using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using RestSharp;

// ReSharper disable ArrangeModifiersOrder

namespace DisneyDown.Common.KeySystem
{
    public class Connection
    {
        public AuthInfo Authentication { get; set; } = new AuthInfo();

        public KeySystemEndpoints Endpoints { get; set; } = new KeySystemEndpoints();

        public ServiceInfo Service { get; set; } = new ServiceInfo();

        /// <summary>
        /// Current directory of the DisneyDown executable
        /// </summary>
        private static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static Connection FromConnectionFile()
        {
            try
            {
                //default connection file location
                var file = $@"{AssemblyDirectory}\cfg\keyserver.json";

                //path of file
                var filePath = Path.GetDirectoryName(file);

                //check if directory doesn't already exist
                if (!Directory.Exists(filePath))

                    //it doesn't so create it
                    Directory.CreateDirectory(filePath ?? string.Empty);

                //does the config file exist?
                if (File.Exists(file))
                {
                    //it does exist, read it in
                    var connection = File.ReadAllText(file);

                    //null validation
                    if (!string.IsNullOrWhiteSpace(connection))
                    {
                        //try deserialisation
                        var connectionObject = JsonConvert.DeserializeObject<Connection>(connection);

                        //null validation
                        if (connectionObject != null)
                        {
                            //valid conversion, return result
                            return connectionObject;
                        }

                        //report error
                        ConsoleWriters.ConsoleWriteError(@"Couldn't load keyserver connection information because deserialisation failed");
                    }
                    else
                    {
                        //report error
                        ConsoleWriters.ConsoleWriteError(@"Couldn't load keyserver connection information because the file isn't valid");
                    }
                }
                else
                {
                    //new serialisation handler
                    var serialiser = new JsonSerializer
                    {
                        Formatting = Formatting.Indented
                    };

                    //new stream for writing
                    using (var sw = new StreamWriter(file))
                    using (var writer = new JsonTextWriter(sw))
                    {
                        //serialise and write to the new file
                        serialiser.Serialize(writer, new Connection());
                    }

                    //return result
                    return new Connection();
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($@"A problem occurred whilst trying to read a keyserver connection file: {ex.Message}");
            }

            //default
            return null;
        }

        public GenericServiceResponse ReportKey(string keyId, string key)
        {
            try
            {
                //null validation
                if (!string.IsNullOrWhiteSpace(keyId) && !string.IsNullOrWhiteSpace(key))
                {
                    //length validation
                    if (keyId.Length == 32 && key.Length == 32)
                    {
                        //setup client
                        var client = new RestClient(Service.BaseService);

                        //setup request
                        var request = new RestRequest(Endpoints.ReportEndpoint, Method.POST);

                        //setup data
                        request.AddParameter(@"authUsername", Authentication.Username);
                        request.AddParameter(@"authPassword",
                            Sha256Helper.CalculateSha256Hash(Authentication.Password));
                        request.AddParameter(@"playbackDomain", @"www.disneyplus.com");
                        request.AddParameter(@"keyId", keyId);
                        request.AddParameter(@"key", key);

                        //execute request
                        var response = client.Execute(request).Content;

                        //newtonsoft converter
                        var responseData = JsonConvert.DeserializeObject<GenericServiceResponse>(response, new JsonSerializerSettings
                        {
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        });

                        //return final result
                        return responseData;
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($@"Failed to report key ID: {keyId}: {ex.Message}");
            }

            //default
            return null;
        }
    }
}