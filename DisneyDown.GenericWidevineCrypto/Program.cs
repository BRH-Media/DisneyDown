using DisneyDown.Common.Util.Kit;
using WVCore;

namespace DisneyDown.GenericWidevineCrypto
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                const string settingsFile = @"wvCryptoConfig.json";
                if (File.Exists(settingsFile))
                {
                    //import JSON config
                    var configJson = File.ReadAllText(settingsFile);

                    //validation
                    if (!string.IsNullOrWhiteSpace(configJson))
                    {
                        //deserialise config data
                        var configData = WVInterfaceConfig.FromJson(configJson);

                        //validation
                        if (!string.IsNullOrWhiteSpace(configData.LicenceServer))
                        {
                            //base64 PSSH data
                            var pssh = args[0];
                            var key = WVInterfaceManager.GetCdmHexKeyForPssh(pssh, configData);
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
                            ConsoleWriters.ConsoleWriteError(@"Invalid configuration data");
                        }
                    }
                    else
                    {
                        ConsoleWriters.ConsoleWriteError(@"Invalid configuration data");
                    }
                }
                else
                {
                    var config = new WVInterfaceConfig().ToJson();
                    File.WriteAllText(settingsFile, config);
                    ConsoleWriters.ConsoleWriteError($"{settingsFile} was just created; please fill it out accordingly");
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