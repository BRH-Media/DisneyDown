using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using System;
using System.IO;

// ReSharper disable RedundantIfElseBlock
// ReSharper disable ReplaceAutoPropertyWithComputedProperty
// ReSharper disable InvertIf

namespace DisneyDown.Common.Globals.FilterValues
{
    public class FilterValuesProvider
    {
        /// <summary>
        /// Stores video, audio and subtitle filters
        /// </summary>
        public FilterValuesMain MainContent { get; } = new FilterValuesMain();

        /// <summary>
        /// "Dub Card" filters make sure no irrelevant segments are processed
        /// </summary>
        public FilterValuesItem DubCard { get; set; } = new FilterValuesItem
        {
            PrimaryFilter =
            {
                FilterString = @"DUB_CARD",
                Enabled = true
            },
            SecondaryFilter =
            {
                FilterString = @"DUB_CARD",
                Enabled = true
            },
            FallbackFilter =
            {
                FilterString = @"DUB_CARD",
                Enabled = false
            }
        };

        /// <summary>
        /// "Bumper intro" filters ensure that the Disney+ Originals intro is detected properly
        /// </summary>
        public FilterValuesItem BumperIntro { get; set; } = new FilterValuesItem
        {
            PrimaryFilter =
            {
                FilterString = @"-BUMPER/",
                Enabled = true
            },
            SecondaryFilter =
            {
                FilterString = @"-BUMPER/",
                Enabled = true
            },
            FallbackFilter =
            {
                FilterString = @"-BUMPER/",
                Enabled = false
            },
        };

        /// <summary>
        /// This will generate a new filter provider and will write the result to a file.
        /// If a file already exists, this file will be read instead of a new provider being generated.
        /// This file may be edited by the user to allow for custom filter values.
        /// </summary>
        /// <returns>A new filter provider</returns>
        public static FilterValuesProvider Init()
        {
            try
            {
                //new filter file
                var newFileJson = JsonConvert.SerializeObject(new FilterValuesProvider(), Formatting.Indented);

                //does a segment filter file exist?
                if (File.Exists(Strings.SegmentFilterConfigurationFile))
                {
                    //debugging output
                    ConsoleWriters.ConsoleWriteDebug($"Reading filter file: {Strings.SegmentFilterConfigurationFile}");

                    //read it into memory
                    var segmentFilter = File.ReadAllText(Strings.SegmentFilterConfigurationFile);

                    //validation
                    if (!string.IsNullOrWhiteSpace(segmentFilter))
                    {
                        //debugging output
                        ConsoleWriters.ConsoleWriteDebug($"Deserialising filter file: {Strings.SegmentFilterConfigurationFile}");

                        //attempt deserialisation
                        var segmentFilterObject = JsonConvert.DeserializeObject<FilterValuesProvider>(segmentFilter);

                        //validation
                        if (segmentFilterObject != null)
                        {
                            //debugging output
                            ConsoleWriters.ConsoleWriteDebug($"Successfully loaded filter file: {Strings.SegmentFilterConfigurationFile}");

                            //return the valid filter segments object
                            return segmentFilterObject;
                        }
                    }
                    else
                    {
                        //delete the file
                        File.Delete(Strings.SegmentFilterConfigurationFile);

                        //create a new file
                        File.WriteAllText(Strings.SegmentFilterConfigurationFile, newFileJson);
                    }
                }
                else
                {
                    //create the file
                    File.WriteAllText(Strings.SegmentFilterConfigurationFile, newFileJson);
                }
            }
            catch (Exception ex)
            {
                //debugging output
                ConsoleWriters.ConsoleWriteDebug($"FilterValues initialisation error: {ex.Message}");
            }

            //default
            return new FilterValuesProvider();
        }
    }
}