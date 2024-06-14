using DisneyDown.Common.API;
using DisneyDown.Common.API.Globals;
using DisneyDown.Common.API.Schemas.ServicesSchema;
using DisneyDown.Common.Util.Kit;
using WVCore;

namespace DisneyDown.WidevineCrypto
{
    internal class Program
    {
        private static string DisneyLicenseServerUrl => @"https://disney.playback.edge.bamgrid.com/widevine/v1/obtain-license";

        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (File.Exists(@"cdrmToken.txt"))
                {
                    var token = File.ReadAllText(@"cdrmToken.txt");

                    //validation
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        if (token.ToLower() == @"obtainvia.login")
                        {
                            //auth package request required
                            ConsoleWriters.ConsoleWriteInfo(@"Authentication via Disney+ login is required");
                            Objects.Services = ServiceInformation.GetServices();
                            var auth = Objects.Configuration.DeviceContext.RequestAuthenticationPackage();
                            if (!string.IsNullOrWhiteSpace(auth?.Account?.OAuthToken?.Token))
                            {
                                token = auth.Account?.OAuthToken?.Token;
                            }
                            else
                            {
                                ConsoleWriters.ConsoleWriteError(@"Failed to retrieve authentication data; licence request cannot proceed");
                                Console.ReadKey();
                                return;
                            }
                        }

                        ConsoleWriters.ConsoleWriteInfo(@"Fetching service certificate");
                        var cert = WVInterfaceManager.GetDisneyWidevineCertificateB64();
                        if (string.IsNullOrWhiteSpace(cert))
                        {
                            ConsoleWriters.ConsoleWriteError(@"Failed to retrieve certificate (null result)");
                            return;
                        }
                        var pssh = args[0];
                        var config = new WVInterfaceConfig
                        {
                            LicenceAuthorization = token ?? @"",
                            LicenceCertificate = cert,
                            LicenceServer = DisneyLicenseServerUrl
                        };
                        var key = WVInterfaceManager.GetCdmHexKeyForPssh(pssh, config);
                        if (!string.IsNullOrWhiteSpace(key))
                        {
                            ConsoleWriters.ConsoleWriteSuccess(
                                @"Success! The Disney+ licensing server has provided you with decryption information");
                            ConsoleWriters.Break();

                            var details = key.Split(@":");
                            ConsoleWriters.ConsoleWriteInfo($"KID: {details[0]}");
                            ConsoleWriters.ConsoleWriteInfo($"KEY: {details[1]}");
                        }
                        else
                        {
                            ConsoleWriters.ConsoleWriteError(@"Failed to retrieve keys");
                        }
                    }
                    else
                    {
                        ConsoleWriters.ConsoleWriteError(@"Invalid token data");
                    }
                }
                else
                {
                    ConsoleWriters.ConsoleWriteError(@"cdrmToken.txt does not exist; please create this file and populate it with your account token");
                }
            }
            else
            {
                ConsoleWriters.ConsoleWriteError(@"Please provide PSSH (start DisneyDown in debug mode to find this)");
            }

            //wait
            Console.ReadKey();
        }
    }
}