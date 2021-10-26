using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using System;
using System.IO;

namespace DisneyDown.Common.ExternalRetrieval
{
    public static class Extensions
    {
        public static bool ToJson(this Endpoints e, string jsonFile)
        {
            try
            {
                //verification
                if (e != null && !string.IsNullOrWhiteSpace(jsonFile))
                {
                    //start serialisation
                    e.SerializeToJson(jsonFile);

                    //report success
                    return true;
                }

                //report error
                ConsoleWriters.ConsoleWriteError(@"Couldn't save endpoints JSON file: invalid parameters");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Couldn't save endpoints JSON file: {ex.Message}");
            }

            //default return
            return false;
        }

        private static void SerializeToJson<T>(this T o, string jsonFilePath)
        {
            //JSON serialiser
            var jsonRepresentation = JsonConvert.SerializeObject(o, Formatting.Indented);

            //serialise straight to the file
            File.WriteAllText(jsonFilePath, jsonRepresentation);
        }

        public static T DeserializeToObject<T>(string filepath) where T : class
            => JsonConvert.DeserializeObject<T>(filepath);
    }
}