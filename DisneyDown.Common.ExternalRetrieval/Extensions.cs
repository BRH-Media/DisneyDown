using DisneyDown.Common.Util.Kit;
using System;
using System.IO;
using System.Xml.Serialization;

namespace DisneyDown.Common.ExternalRetrieval
{
    public static class Extensions
    {
        public static bool ToXml(this Endpoints e, string xmlFile)
        {
            try
            {
                //verification
                if (e != null && !string.IsNullOrWhiteSpace(xmlFile))
                {
                    //start serialisation
                    e.SerializeToXml(xmlFile);

                    //report success
                    return true;
                }

                //report error
                ConsoleWriters.ConsoleWriteError(@"Couldn't save endpoints XML file: invalid parameters");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Couldn't save endpoints XML file: {ex.Message}");
            }

            //default return
            return false;
        }

        private static void SerializeToXml<T>(this T o, string xmlFilePath)
        {
            //XML serialisation handler
            var xmlSerializer = new XmlSerializer(o.GetType());

            //file handler
            using (var writer = new StreamWriter(xmlFilePath))

                //serialise straight to the file
                xmlSerializer.Serialize(writer, o);
        }

        public static T DeserializeToObject<T>(string filepath) where T : class
        {
            //XML serialisation handler
            var ser = new XmlSerializer(typeof(T));

            //file handler
            using (var sr = new StreamReader(filepath))

                //deserialise and return the final object result
                return (T)ser.Deserialize(sr);
        }
    }
}