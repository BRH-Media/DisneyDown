using DisneyDown.Common.Globals;
using DisneyDown.Common.Util;
using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

// ReSharper disable RedundantIfElseBlock

namespace DisneyDown.Common.Schemas
{
    /// <summary>
    /// Stores bandwidth rating information used for manifest quality sorting
    /// </summary>
    public class BandwidthDefinitions
    {
        public static BandwidthDefinitions FromJson(string json) =>
            JsonConvert.DeserializeObject<BandwidthDefinitions>(json, Converter.Settings);

        public static BandwidthDefinitions GetBandwidthDefinitions(bool verbose = true)
        {
            //console verbosity setting
            ConsoleWriters.DisableAllOutput = !verbose;

            try
            {
                //directory verification
                if (!Directory.Exists(Path.GetDirectoryName(Strings.BandwidthConfigurationFile)))
                {
                    //create directory
                    Directory.CreateDirectory(Path.GetDirectoryName(Strings.BandwidthConfigurationFile) ?? @"cfg");
                }

                //is there a bandwidth definitions JSON already present?
                if (File.Exists(Strings.BandwidthConfigurationFile))
                {
                    //alert user
                    ConsoleWriters.ConsoleWriteInfo($"Loading bandwidth definitions from file: {Strings.BandwidthConfigurationFile}");

                    //load it instead of downloading a new copy
                    var bandwidthJson = File.ReadAllText(Strings.BandwidthConfigurationFile);

                    //verification
                    if (!string.IsNullOrWhiteSpace(bandwidthJson))
                    {
                        //deserialisation
                        var bandwidth = FromJson(bandwidthJson);

                        //validation
                        if (bandwidth != null)
                        {
                            //restore verbosity setting to default
                            ConsoleWriters.DisableAllOutput = false;

                            //return the result
                            return bandwidth;
                        }
                        else
                        {
                            //alert user
                            ConsoleWriters.ConsoleWriteError(@"Invalid bandwidth definitions: null object");
                        }
                    }
                    else
                    {
                        //delete the invalid file
                        File.Delete(Strings.BandwidthConfigurationFile);

                        //retry operations
                        return GetBandwidthDefinitions();
                    }
                }
                else
                {
                    //alert user
                    ConsoleWriters.ConsoleWriteInfo(@"No file found; using and exporting default bandwidth definitions");

                    //download a new copy
                    var bandwidthDefinitions = new BandwidthDefinitions();

                    //serialise the object to formatted JSON
                    var formattedJson = JsonConvert.SerializeObject(bandwidthDefinitions, Formatting.Indented);

                    //save to a file
                    File.WriteAllText(Strings.BandwidthConfigurationFile, formattedJson);

                    //alert user
                    ConsoleWriters.ConsoleWriteInfo($"Saved bandwidth definitions to file: {Strings.BandwidthConfigurationFile}");

                    //retry operations
                    return GetBandwidthDefinitions();
                }
            }
            catch (Exception ex)
            {
                //handle error
                ConsoleWriters.ConsoleWriteError($"Error retrieving bandwidth definitions: {ex.Message}");
            }

            //restore verbosity setting to default
            ConsoleWriters.DisableAllOutput = false;

            //default
            return null;
        }

        public Dictionary<int, QualityRating> VideoDefinitions { get; set; } = new Dictionary<int, QualityRating>
        {
            {
                10,
                new QualityRating(@"8500",@"3840x2160 UHD-HDR")
            },
            {
                9,
                new QualityRating(@"7000",@"2560x1440 QHD-HDR")
            },
            {
                8,
                new QualityRating(@"5500",@"1920x1080 FHD-HDR")
            },
            {
                7,
                new QualityRating(@"4250",@"1280x720 HD-SDR")
            },
            {
                6,
                new QualityRating(@"3000",@"1280x720 HD-SDR")
            },
            {
                5,
                new QualityRating(@"2400",@"1280x720 HD-SDR")
            },
            {
                4,
                new QualityRating(@"1800",@"854x480 SD-SDR")
            },
            {
                3,
                new QualityRating(@"1200",@"854x480 nHD-SDR")
            },
            {
                2,
                new QualityRating(@"800",@"640x360 nHD-SDR")
            },
            {
                1,
                new QualityRating(@"480",@"640x360 nHD-SDR")
            },
            {
                0,
                new QualityRating(@"400",@"640x360 nHD-SDR")
            }
        };

        public Dictionary<int, QualityRating> AudioDefinitions { get; set; } = new Dictionary<int, QualityRating>
        {
            {
                8,
                new QualityRating(@"1000",@"1Mbps")
            },
            {
                7,
                new QualityRating(@"640",@"640kbps")
            },
            {
                6,
                new QualityRating(@"512",@"512kbps")
            },
            {
                5,
                new QualityRating(@"480",@"480kbps")
            },
            {
                4,
                new QualityRating(@"400",@"400kbps")
            },
            {
                3,
                new QualityRating(@"300",@"300kbps")
            },
            {
                2,
                new QualityRating(@"256",@"256kbps")
            },
            {
                1,
                new QualityRating(@"128",@"128kbps")
            },
            {
                0,
                new QualityRating(@"64",@"64kbps")
            }
        };
    }
}