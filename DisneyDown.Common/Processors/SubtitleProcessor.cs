using DisneyDown.Common.Util.Kit;
using System;
using System.Collections.Generic;
using System.Text;

// ReSharper disable InvertIf

namespace DisneyDown.Common.Processors
{
    public static class SubtitleProcessor
    {
        /// <summary>
        /// Determines if the current WebVTT line is a timecode line (no settings)
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static bool IsTimecode(string line)

            //timecode lines always have this unique string
            => line.Contains("-->");

        private static string DeleteCueSettings(string line)
        {
            try
            {
                //cleaned line output
                var output = new StringBuilder();

                //go through each character in the supplied WebVTT line
                foreach (var ch in line)
                {
                    //convert the character to lowercase
                    var chLower = char.ToLower(ch);

                    //if the character is in the alphabetical unicode range (061-122)
                    if (chLower >= 'a' && chLower <= 'z')

                        //break the loop; character is alphabetical
                        break;

                    //character is not alphabetical; append to the buffer
                    output.Append(ch);
                }

                //return the final cleaned line
                return output.ToString();
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Subtitle conversion error: {ex.Message}");
            }

            //default
            return @"";
        }

        public static List<string> ConvertToSrt(List<string> fileLines)
        {
            try
            {
                //converted SubRip Text (SRT) lines are stored here
                var output = new List<string>();

                //current WebVTT line for SubRip timecode counting
                var lineNumber = 1;

                //go through each line in the WebVTT array
                for (var i = 0; i < fileLines.Count; i++)
                {
                    //the current line
                    var line = fileLines[i];

                    //is this the final iteration?
                    var endOfStream = i == fileLines.Count - 1;

                    //only operate on the line if it's a valid timecode line
                    if (IsTimecode(line))
                    {
                        //add the current line number for SRT
                        output.Add(lineNumber.ToString());

                        //increment the current line
                        lineNumber++;

                        //convert WebVTT timecode styling to SRT
                        line = line?.Replace('.', ',');

                        //remove all Cue settings/styling information (if any)
                        line = DeleteCueSettings(line);

                        //add the new timecode
                        output.Add(line);

                        //whether an associated caption was found for the current timecode
                        var foundCaption = false;

                        //a copy of the current array index in order to allow for modification
                        var n = i;

                        //endless loop until a break condition is satisfied
                        while (true)
                        {
                            //is this the final line iteration in the WebVTT file?
                            if (endOfStream)

                                //have we already found a caption for this timecode?
                                if (foundCaption)

                                    //condition satisfied; exit loop
                                    break;

                            //advance the current line by 1
                            line = fileLines[n + 1];

                            //increment the line index counter
                            n++;

                            //ensure the line isn't null or whitespace (tabs, spaces, etc.)
                            if (string.IsNullOrWhiteSpace(line))
                            {
                                //new empty line to separate captions in the resulting SRT file
                                output.Add(@"");

                                //condition satisfied; exit loop
                                break;
                            }

                            //timecode caption found
                            foundCaption = true;

                            //add the resulting line to the buffer
                            output.Add(line);
                        }
                    }
                }

                //go through each line in the output buffer
                for (var i = 0; i < output.Count; i++)
                {
                    //make sure we're operating on a valid timecode
                    if (IsTimecode(output[i]))
                    {
                        //parse out the end time
                        var endTime = output[i].Substring(output[i].IndexOf('>') + 2,
                            output[i].LastIndexOf(",", StringComparison.Ordinal) - (output[i].IndexOf('>') + 2));
                        try
                        {
                            var oldLine = output[i];

                            var ts = TimeSpan.Parse(endTime);

                            if (endTime != ts.ToString())
                                output[i] = oldLine.Replace("--> ", "--> 00:");

                            var startTime = output[i].Substring(0, output[i].IndexOf(",", StringComparison.Ordinal));
                            var ts2 = TimeSpan.Parse(startTime);

                            if (startTime != ts2.ToString())
                                output[i] = output[i].Replace(output[i], "00:" + output[i]);
                        }
                        catch (Exception)
                        {
                            //do nothing
                        }
                    }
                }

                return output;
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Subtitle VTT-SRT error: {ex.Message}");
            }

            //default
            return new List<string>();
        }
    }
}