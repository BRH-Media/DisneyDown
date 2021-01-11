using System;
using System.Collections.Generic;
using System.Text;

// ReSharper disable InvertIf

namespace DisneyDown.Common.Processors
{
    public static class SubtitleProcessor
    {
        private static bool IsTimecode(string line)
            => line.Contains("-->");

        public static string DeleteCueSettings(string line)
        {
            var output = new StringBuilder();
            foreach (var ch in line)
            {
                var chLower = char.ToLower(ch);

                if (chLower >= 'a' && chLower <= 'z')
                    break;

                output.Append(ch);
            }
            return output.ToString();
        }

        public static List<string> ConvertToSrt(List<string> fileLines)
        {
            try
            {
                var output = new List<string>();
                var lineNumber = 1;

                for (var i = 0; i < fileLines.Count; i++)
                {
                    var line = fileLines[i];
                    var endOfStream = i == fileLines.Count - 1;

                    if (IsTimecode(line))
                    {
                        output.Add(lineNumber.ToString());
                        lineNumber++;
                        line = line?.Replace('.', ',');
                        line = DeleteCueSettings(line);
                        output.Add(line);

                        var foundCaption = false;

                        var n = i;
                        while (true)
                        {
                            if (endOfStream)
                            {
                                if (foundCaption)
                                    break;
                            }

                            line = fileLines[n + 1];
                            n++;
                            if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
                            {
                                output.Add(@"");
                                break;
                            }

                            foundCaption = true;
                            output.Add(line);
                        }
                    }
                }

                // time repair
                for (var i = 0; i < output.Count; i++)
                {
                    if (IsTimecode(output[i]))
                    {
                        var secondTime = output[i].Substring(output[i].IndexOf('>') + 2,
                            output[i].LastIndexOf(",", StringComparison.Ordinal) - (output[i].IndexOf('>') + 2));
                        try
                        {
                            var oldLine = output[i];

                            var ts = TimeSpan.Parse(secondTime);

                            if (secondTime != ts.ToString())
                                output[i] = oldLine.Replace("--> ", "--> 00:");

                            var firstTime = output[i].Substring(0, output[i].IndexOf(",", StringComparison.Ordinal));
                            var ts2 = TimeSpan.Parse(firstTime);

                            if (firstTime != ts2.ToString())
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
                Console.WriteLine($"Subtitle VTT-SRT error: {ex.Message}");
            }

            //default
            return new List<string>();
        }
    }
}