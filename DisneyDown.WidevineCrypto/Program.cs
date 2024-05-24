using DisneyDown.Common.Util.Kit;
using DisneyDown.Common.Util.Kit.Enums;
using WVCore;

namespace DisneyDown.WidevineCrypto
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (File.Exists(@"cdrmToken.txt"))
                {
                    var token = File.ReadAllText(@"cdrmToken.txt");
                    var pssh = args[0];
                    var key = WVInterfaceManager.GetCdmHexKeyForPssh(pssh, token);
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        ConsoleWriters.ConsoleWriteSuccess(@"Success! The licensing server has provided you with decryption information");
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