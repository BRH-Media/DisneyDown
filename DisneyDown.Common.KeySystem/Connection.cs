using System;
using System.IO;
using System.Net;
using System.Reflection;
using DisneyDown.Common.KeySystem.Schemas;
using DisneyDown.Common.KeySystem.Schemas.KeySchema;
using DisneyDown.Common.KeySystem.Schemas.UserSchema;
using DisneyDown.Common.Security.Hashing;
using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using RestSharp;

// ReSharper disable IdentifierTypo
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

        public static void KeyServerResponseLog(Status r)
        {
            try
            {
                //validation
                if (r != null)
                {
                    //specific messages
                    switch (r.Code)
                    {
                        case StatusCode.OPERATION_FAILED:
                            ConsoleWriters.ConsoleWriteError(
                                $@"Key server error: {r.Message}");
                            break;

                        case StatusCode.OPERATION_SUCCESS:
                            ConsoleWriters.ConsoleWriteSuccess(@"Successfully contacted key server");
                            break;

                        case StatusCode.ACCESS_DENIED:
                            ConsoleWriters.ConsoleWriteError(@"Key server denied access");
                            break;

                        case StatusCode.UNKNOWN_ERROR:
                            ConsoleWriters.ConsoleWriteError(
                                $@"Key server internal error: {r.Message}");
                            break;

                        case StatusCode.KEY_EXISTS:
                            ConsoleWriters.ConsoleWriteError($@"Key already exists on server");
                            break;

                        case StatusCode.KEY_DOES_NOT_EXIST:
                            break;

                        case StatusCode.WHITELIST_ALLOWED:
                            break;

                        case StatusCode.WHITELIST_DISALLOWED:
                            break;

                        case StatusCode.NOT_WHITELISTED:
                            break;

                        case StatusCode.USER_AUTHORISED:
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

        public KeyResponseContents GetAuthorisedKeys(User authorisedUser, bool verbose = true)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(Authentication.Password) &&
                    !string.IsNullOrWhiteSpace(Authentication.Username) && !string.IsNullOrWhiteSpace(Service.BaseService))
                {
                    //setup client
                    var client = new RestClient(Service.BaseService)
                    {
                        Proxy = WebRequest.DefaultWebProxy
                    };

                    //setup request
                    var request = new RestRequest(authorisedUser.IsAdmin ? Endpoints.SystemQueryListEndpoint : Endpoints.UserQueryListEndpoint, Method.POST);

                    //calculate password to authenticate with
                    var userPassword = Sha256Helper.CalculateSha256Hash(Authentication.Password).ToLower();

                    //setup data
                    request.AddParameter(@"authUsername", Authentication.Username);
                    request.AddParameter(@"authPassword", userPassword);

                    //execute request
                    var response = client.Execute(request).Content;

                    //ConsoleWriters.WriteLine(response);

                    //newtonsoft converter
                    var responseData = JsonConvert.DeserializeObject<KeyResponse>(response, new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    //verbosity
                    if (verbose)

                        //report status
                        KeyServerResponseLog(responseData?.Response?.Status);

                    //return user
                    return responseData?.Response;
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($@"A problem occurred whilst trying to query the key server: {ex.Message}");
            }

            //default
            return null;
        }

        public UserResponseContents GetCurrentUser(bool verbose = true)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(Authentication.Password) &&
                    !string.IsNullOrWhiteSpace(Authentication.Username) && !string.IsNullOrWhiteSpace(Service.BaseService))
                {
                    //setup client
                    var client = new RestClient(Service.BaseService)
                    {
                        Proxy = WebRequest.DefaultWebProxy
                    };

                    //setup request
                    var request = new RestRequest(Endpoints.UserLoginEndpoint, Method.POST);

                    //calculate password to authenticate with
                    var userPassword = Sha256Helper.CalculateSha256Hash(Authentication.Password).ToLower();

                    //setup data
                    request.AddParameter(@"authUsername", Authentication.Username);
                    request.AddParameter(@"authPassword", userPassword);

                    //execute request
                    var response = client.Execute(request).Content;

                    //ConsoleWriters.WriteLine(response);

                    //newtonsoft converter
                    var responseData = JsonConvert.DeserializeObject<UserResponse>(response, new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    //verbosity
                    if (verbose)

                        //report status
                        KeyServerResponseLog(responseData?.Response?.Status);

                    //return user
                    return responseData?.Response;
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($@"A problem occurred whilst trying to authenticate with the key server: {ex.Message}");
            }

            //default
            return null;
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
                        var client = new RestClient(Service.BaseService)
                        {
                            Proxy = WebRequest.DefaultWebProxy
                        };

                        //setup request
                        var request = new RestRequest(Endpoints.UserQueryEndpoint, Method.POST);

                        //setup data
                        request.AddParameter(@"authUsername", Authentication.Username);
                        request.AddParameter(@"authPassword",
                            Sha256Helper.CalculateSha256Hash(Authentication.Password).ToLower());
                        request.AddParameter(@"playbackDomain", @"www.disneyplus.com");
                        request.AddParameter(@"keyId", keyId);

                        //execute request
                        var response = client.Execute(request).Content;

                        //newtonsoft converter
                        var responseData = JsonConvert.DeserializeObject<KeyResponse>(response, new JsonSerializerSettings
                        {
                            MissingMemberHandling = MissingMemberHandling.Ignore,
                            NullValueHandling = NullValueHandling.Ignore
                        });

                        //verbosity
                        if (verbose)

                            //report status
                            KeyServerResponseLog(responseData?.Response?.Status);

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

        public KeyResponse ReportKey(string keyId, string key, bool verbose = true)
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
                        var client = new RestClient(Service.BaseService)
                        {
                            Proxy = WebRequest.DefaultWebProxy
                        };

                        //setup request
                        var request = new RestRequest(Endpoints.ReportEndpoint, Method.POST);

                        //setup data
                        request.AddParameter(@"authUsername", Authentication.Username);
                        request.AddParameter(@"authPassword",
                            Sha256Helper.CalculateSha256Hash(Authentication.Password).ToLower());
                        request.AddParameter(@"playbackDomain", @"www.disneyplus.com");
                        request.AddParameter(@"keyId", keyId);
                        request.AddParameter(@"key", key);

                        //execute request
                        var response = client.Execute(request).Content;

                        //newtonsoft converter
                        var responseData = JsonConvert.DeserializeObject<KeyResponse>(response, new JsonSerializerSettings
                        {
                            MissingMemberHandling = MissingMemberHandling.Ignore,
                            NullValueHandling = NullValueHandling.Ignore
                        });

                        //verbosity
                        if (verbose)

                            //report status
                            KeyServerResponseLog(responseData?.Response?.Status);

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