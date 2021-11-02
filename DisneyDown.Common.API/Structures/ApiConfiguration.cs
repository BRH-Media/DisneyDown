using DisneyDown.Common.API.Globals;
using DisneyDown.Common.API.Structures.ApiDevice;
using DisneyDown.Common.Util;
using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using System;
using System.IO;

// ReSharper disable RedundantIfElseBlock

namespace DisneyDown.Common.API.Structures
{
    public class ApiConfiguration
    {
        public string ServicesUrl { get; set; } = Strings.DefaultServicesUrl;
        public ApiConfigurationCredentials Credentials { get; set; } = new ApiConfigurationCredentials();
        public ApiDevice.ApiDevice DeviceContext { get; set; } = new ApiDeviceChrome();

        public static ApiConfiguration FromJson(string json) =>
            JsonConvert.DeserializeObject<ApiConfiguration>(json, Converter.Settings);

        public static ApiConfiguration GetApiConfiguration(bool verbose = true)
        {
            //console verbosity setting
            ConsoleWriters.DisableAllOutput = !verbose;

            try
            {
                //directory verification
                if (!Directory.Exists(Path.GetDirectoryName(Strings.ConfigurationFile)))
                {
                    //create directory
                    Directory.CreateDirectory(Path.GetDirectoryName(Strings.ConfigurationFile) ?? @"cfg");
                }

                //is there an API configuration JSON file already present?
                if (File.Exists(Strings.ConfigurationFile))
                {
                    //load the JSON content
                    var configJson = File.ReadAllText(Strings.ConfigurationFile);

                    //verification
                    if (!string.IsNullOrWhiteSpace(configJson))
                    {
                        //deserialisation
                        var config = FromJson(configJson);

                        //validation
                        if (config != null)
                        {
                            //restore verbosity setting to default
                            ConsoleWriters.DisableAllOutput = false;

                            //return the result
                            return config;
                        }
                        else
                        {
                            //alert user
                            ConsoleWriters.ConsoleWriteError(@"Invalid API configuration: null object");
                        }
                    }
                    else
                    {
                        //delete the invalid file
                        File.Delete(Strings.ConfigurationFile);

                        //retry operations
                        return GetApiConfiguration();
                    }
                }
                else
                {
                    //create a new configuration object
                    var configuration = new ApiConfiguration();

                    //serialise the object to formatted JSON
                    var formattedJson = JsonConvert.SerializeObject(configuration, Formatting.Indented);

                    //save to a file
                    File.WriteAllText(Strings.ConfigurationFile, formattedJson);

                    //alert user
                    ConsoleWriters.ConsoleWriteInfo($"Saved new API configuration to file: {Strings.ConfigurationFile}");

                    //retry operations
                    return GetApiConfiguration();
                }
            }
            catch (Exception ex)
            {
                //handle error
                ConsoleWriters.ConsoleWriteError($"Error retrieving API configuration: {ex.Message}");
            }

            //restore verbosity setting to default
            ConsoleWriters.DisableAllOutput = false;

            //default
            return null;
        }
    }
}