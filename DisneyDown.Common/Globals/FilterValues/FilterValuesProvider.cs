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
        public FilterValuesMain MainContent { get; } = new FilterValuesMain();

        public FilterValuesItem DubCard { get; set; } = new FilterValuesItem
        {
            PrimaryFilter = @"DUB_CARD",
            SecondaryFilter = @"DUB_CARD",
            FallbackFilter = @"DUB_CARD"
        };

        public FilterValuesItem BumperIntro { get; set; } = new FilterValuesItem
        {
            PrimaryFilter = @"-BUMPER/",
            SecondaryFilter = @"-BUMPER/",
            FallbackFilter = @"-BUMPER/"
        };

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