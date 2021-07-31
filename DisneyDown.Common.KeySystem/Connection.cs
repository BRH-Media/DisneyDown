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

        public static void KeyServerResponseLog(GenericServiceResponse r)
        {
            try
            {
                //validation
                if (r != null)
                {
                    //specific messages
                    switch (r.Response.Status.Code)
                    {
                        case StatusCode.OPERATION_FAILED:
                            ConsoleWriters.ConsoleWriteError(
                                $@"Key server error: {r.Response.Status.Message}");
                            break;

                        case StatusCode.OPERATION_SUCCESS:
                            ConsoleWriters.ConsoleWriteSuccess(@"Successfully contacted key server");
                            break;

                        case StatusCode.ACCESS_DENIED:
                            ConsoleWriters.ConsoleWriteError(@"Key server denied access");
                            break;

                        case StatusCode.UNKNOWN_ERROR:
                            ConsoleWriters.ConsoleWriteError(
                                $@"Key server internal error: {r.Response.Status.Message}");
                            break;

                        case StatusCode.KEY_EXISTS:
                            ConsoleWriters.ConsoleWriteError($@"Key already exists on server");
                            break;

                        default:
                            ConsoleWriters.ConsoleWriteError(@"Unknown key server status");
                            break;
                    }
                }
                else
                {
                    //report error
                    ConsoleWriters.ConsoleWriteError(
                        @"Key server error: Response information was null");
                }
            }
            catch (Exception ex)
            {
                //report error
                //report error
                ConsoleWriters.ConsoleWriteError(
                    $@"Key server error: {ex.Message}");
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

        public StoredKey FindKey(string keyId, bool verbose = true)
        {
            try
            {
                //null validation
                if (!string.IsNullOrWhiteSpace(keyId))
                {
                    //length validation
                    if (keyId.Length == 32)
                    {
                        //setup client
                        var client = new RestClient(Service.BaseService);

                        //setup request
                        var request = new RestRequest(Endpoints.UserQueryEndpoint, Method.POST);

                        //setup data
                        request.AddParameter(@"authUsername", Authentication.Username);
                        request.AddParameter(@"authPassword",
                            Sha256Helper.CalculateSha256Hash(Authentication.Password));
                        request.AddParameter(@"playbackDomain", @"www.disneyplus.com");
                        request.AddParameter(@"keyId", keyId);

                        //execute request
                        var response = client.Execute(request).Content;

                        //newtonsoft converter
                        var responseData = JsonConvert.DeserializeObject<GenericServiceResponse>(response, new JsonSerializerSettings
                        {
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        });

                        //verbosity
                        if (verbose)

                            //report status
                            KeyServerResponseLog(responseData);

                        //second-level null validation
                        if (responseData?.Response?.Data?.Length > 0)

                            //return final key
                            return responseData.Response.Data[0];
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($@"Failed to locate a key for key ID: {keyId}: {ex.Message}");
            }

            //default
            return null;
        }

        public GenericServiceResponse ReportKey(string keyId, string key, bool verbose = true)
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

                        //verbosity
                        if (verbose)

                            //report status
                            KeyServerResponseLog(responseData);

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