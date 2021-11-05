using DisneyDown.Common.API.Globals;
using DisneyDown.Common.Net;
using DisneyDown.Common.Util;
using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using System;
using System.IO;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class ServiceInformation
    {
        [JsonProperty("application")]
        public Application Application { get; set; }

        [JsonProperty("commonHeaders")]
        public CommonHeaders CommonHeaders { get; set; }

        [JsonProperty("logger")]
        public Logger Logger { get; set; }

        [JsonProperty("services")]
        public Services Services { get; set; }

        public static ServiceInformation FromJson(string json) =>
            JsonConvert.DeserializeObject<ServiceInformation>(json, ApiJsonConverter.Settings);

        //retrieves the services JSON content from a URL or a file
        public static ServiceInformation GetServices(bool verbose = true)
        {
            //console verbosity setting
            ConsoleWriters.DisableAllOutput = !verbose;

            try
            {
                //directory verification
                if (!Directory.Exists(Path.GetDirectoryName(Strings.ServicesFile)))
                {
                    //create directory
                    Directory.CreateDirectory(Path.GetDirectoryName(Strings.ServicesFile) ?? @"cfg");
                }

                //is there a services JSON already present?
                if (File.Exists(Strings.ServicesFile))
                {
                    //alert user
                    ConsoleWriters.ConsoleWriteInfo($"Loading services catalog from file: {Strings.ServicesFile}");

                    //load it instead of downloading a new copy
                    var servicesJson = File.ReadAllText(Strings.ServicesFile);

                    //verification
                    if (!string.IsNullOrWhiteSpace(servicesJson))
                    {
                        //deserialisation
                        var services = FromJson(servicesJson);

                        //validation
                        if (services != null)
                        {
                            //restore verbosity setting to default
                            ConsoleWriters.DisableAllOutput = false;

                            //return the result
                            return services;
                        }
                        else
                        {
                            //alert user
                            ConsoleWriters.ConsoleWriteError(@"Invalid services catalog: null object");
                        }
                    }
                    else
                    {
                        //delete the invalid file
                        File.Delete(Strings.ServicesFile);

                        //retry operations
                        return GetServices();
                    }
                }
                else
                {
                    //alert user
                    ConsoleWriters.ConsoleWriteInfo(@"Download new services catalog from Disney+");

                    //download a new copy
                    var servicesJson = ResourceGrab.GrabString(Objects.Configuration.ServicesUrl);

                    //validation
                    if (!string.IsNullOrWhiteSpace(servicesJson))
                    {
                        //format the ugly JSON
                        var formattedJson = JsonUtil.JsonPrettify(servicesJson);

                        //save to a file
                        File.WriteAllText(Strings.ServicesFile, formattedJson);

                        //alert user
                        ConsoleWriters.ConsoleWriteInfo($"Saved services catalog to file: {Strings.ServicesFile}");

                        //retry operations
                        return GetServices();
                    }
                    else
                    {
                        //report error
                        ConsoleWriters.ConsoleWriteError(@"Experienced an error whilst trying to download the service information catalog from Disney+");
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                ConsoleWriters.ConsoleWriteError($"Error retrieving Disney+ services: {ex.Message}");
            }

            //restore verbosity setting to default
            ConsoleWriters.DisableAllOutput = false;

            //default
            return null;
        }
    }
}