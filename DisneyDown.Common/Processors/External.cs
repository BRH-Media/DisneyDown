using DisneyDown.Common.Globals;
using DisneyDown.Common.Schemas;
using DisneyDown.Common.Util.Kit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

// ReSharper disable InconsistentNaming
// ReSharper disable InvertIf

namespace DisneyDown.Common.Processors
{
    /// <summary>
    /// External program wrappers for FFMPEG and bento4 mp4decrypt
    /// </summary>
    public static class External
    {
        /// <summary>
        /// Bento4 mp4decrypt wrapper for decrypting singular files with a Widevine key
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="outputFile"></param>
        /// <param name="key"></param>
        /// <param name="forceOverwrite"></param>
        public static void DoDecrypt(string inputFile, string outputFile, string key, bool forceOverwrite = false)
        {
            try
            {
                var fileName = $@"{Strings.AssemblyDirectory}\mp4decrypt.exe";

                if (File.Exists(fileName))
                {
                    if (!File.Exists(outputFile) || forceOverwrite)
                    {
                        //declare bento4 executable startup
                        var p = new Process
                        {
                            StartInfo =
                            {
                                FileName = fileName,
                                WindowStyle = ProcessWindowStyle.Hidden,
                                Arguments = $"--key 1:{key} --show-progress \"{inputFile}\" \"{outputFile}\""
                            }
                        };

                        //execute the decryptor
                        p.Start();

                        //don't continue until the decrypter quits
                        p.WaitForExit();
                    }
                    else

                        //report error
                        ConsoleWriters.ConsoleWriteError(@"Decrypt failed; output file specified already exists");
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError($"Couldn't decrypt because {fileName} was not found");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Decryption error: {ex.Message}");
            }
        }

        /// <summary>
        /// FFMPEG wrapper for remuxing audio and video files
        /// </summary>
        /// <param name="inputFiles"></param>
        /// <param name="outputFile"></param>
        /// <param name="forceOverwrite"></param>
        public static void DoMux(List<string> inputFiles, string outputFile = @"content.mkv", bool forceOverwrite = false)
        {
            try
            {
                //FFMPEG executable
                var fileName = $@"{Strings.AssemblyDirectory}\ffmpeg.exe";

                //does FFMPEG exist?
                if (File.Exists(fileName))
                {
                    //if the output file doesn't exist or an overwrite is forced
                    if (!File.Exists(outputFile) || forceOverwrite)
                    {
                        //loop through each input file to build the string
                        var inputFileString = inputFiles.Aggregate(@"", (current, s) => current + $" -i \"{s}\"");

                        //declare FFMPEG executable startup
                        var p = new Process
                        {
                            StartInfo =
                            {
                                FileName = fileName,
                                WindowStyle = ProcessWindowStyle.Hidden,
                                Arguments = $"{inputFileString.TrimStart(' ')} -c copy \"{outputFile}\""
                            }
                        };

                        //execute FFMPEG
                        p.Start();

                        //don't continue until FFMPEG quits
                        p.WaitForExit();
                    }
                    else

                        //report error
                        ConsoleWriters.ConsoleWriteError(@"Remux failed; output file specified already exists");
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError($"Couldn't remux because {fileName} was not found");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"FFMPEG Remux error: {ex}");
            }
        }

        public static void WriteKeyIdFile(string keyId, string outputFile)
        {
            try
            {
                //ensure valid key ID
                if (!string.IsNullOrWhiteSpace(keyId) && keyId.Length == 32)
                {
                    //ensure directory exists
                    if (!Directory.Exists(Path.GetDirectoryName(outputFile)))

                        //create it
                        Directory.CreateDirectory(Path.GetDirectoryName(outputFile) ?? string.Empty);

                    //write
                    File.WriteAllText(outputFile, keyId);
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Error occurred whilst creating key ID file: {ex}");
            }
        }

        public static void GetKeyIdFromMp4(string inputFile, string outputFile)
            => WriteKeyIdFile(GetKeyIdFromMp4(inputFile), outputFile);

        public static string GetKeyIdFromMp4(string inputFile)
        {
            try
            {
                //MP4Dump executable
                var fileName = $@"{Strings.AssemblyDirectory}\mp4dump.exe";

                //does MP4Dump exist?
                if (File.Exists(fileName))
                {
                    //declare MP4Dump executable startup
                    var process = new Process
                    {
                        StartInfo =
                            {
                                FileName = fileName,
                                CreateNoWindow = true,
                                RedirectStandardOutput = true,
                                UseShellExecute = false,
                                Arguments = $"--format json \"{inputFile}\""
                            }
                    };

                    //execute MP4Dump
                    process.Start();

                    //JSON result
                    var finalJson = "";

                    //capture standard output
                    while (!process.StandardOutput.EndOfStream)
                    {
                        //read in new line
                        finalJson += process.StandardOutput.ReadLine() + "\n";
                    }

                    //parse JSON
                    var jsonObject = Mp4DumpTopLevelAtom.FromJson(finalJson);

                    //through each atom
                    foreach (var a in jsonObject)
                    {
                        //movie atom
                        if (a.Name == "moov")
                        {
                            foreach (var m in a.Children)
                            {
                                //track atom
                                if (m.Name == "trak")
                                {
                                    foreach (var d in m.Children)
                                    {
                                        //media atom
                                        if (d.Name == "mdia")
                                        {
                                            foreach (var e in d.Children)
                                            {
                                                //media information atom
                                                if (e.Name == "minf")
                                                {
                                                    foreach (var i in e.Children)
                                                    {
                                                        //static table atom
                                                        if (i.Name == "stbl")
                                                        {
                                                            foreach (var s in i.Children)
                                                            {
                                                                if (s.Name == "stsd")
                                                                {
                                                                    foreach (var t in s.Children)
                                                                    {
                                                                        if (t.Name == "encv")
                                                                        {
                                                                            foreach (var p in t.Children)
                                                                            {
                                                                                if (p.Name == "sinf")
                                                                                {
                                                                                    foreach (var k in p.Children)
                                                                                    {
                                                                                        if (k.Name == "schi")
                                                                                        {
                                                                                            foreach (var j in k.Children
                                                                                            )
                                                                                            {
                                                                                                if (j.Name == "tenc")
                                                                                                {
                                                                                                    //key ID
                                                                                                    var rawKeyId =
                                                                                                        j.DefaultKid.Replace(" ", "").Replace("[", "").Replace("]", "").ToLower();

                                                                                                    //return result
                                                                                                    return rawKeyId;
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError($"Couldn't extract key ID because {fileName} was not found");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"MP4Dump extraction error: {ex}");
            }

            //default value
            return @"";
        }

        private static void WriteMergeList(string filePath, IReadOnlyCollection<string> files)
        {
            try
            {
                //null validation
                if (files != null)

                    //ensure there are file paths to write
                    if (files.Count > 0)
                    {
                        //store all paths to write
                        var mergeList = files.Select(f => $@"file '{f}'").ToList();

                        //export to the file
                        File.WriteAllLines(filePath, mergeList);
                    }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($@"Merge file write error: {ex.Message}");
            }
        }

        /// <summary>
        /// FFMPEG wrapper for remuxing audio and video files
        /// </summary>
        /// <param name="inputFiles"></param>
        /// <param name="outputFile"></param>
        /// <param name="forceOverwrite"></param>
        public static void DoConcatMux(List<string> inputFiles, string outputFile = @"content.mp4", bool forceOverwrite = false)
        {
            try
            {
                //concatenation list file
                var tmpListFile = $@"{Path.GetDirectoryName(outputFile)}\mergeDef";

                //FFMPEG executable to run
                var fileName = $@"{Strings.AssemblyDirectory}\ffmpeg.exe";

                //ensure FFMPEG exists
                if (File.Exists(fileName))
                {
                    //ensure the file doesn't get overwritten (unless the flag is provided to be true)
                    if (!File.Exists(outputFile) || forceOverwrite)
                    {
                        //save the merge list
                        WriteMergeList(tmpListFile, inputFiles);

                        //declare FFMPEG executable startup
                        var p = new Process
                        {
                            StartInfo =
                            {
                                FileName = fileName,
                                WindowStyle = ProcessWindowStyle.Hidden,
                                Arguments = $"-f concat -safe 0 -i \"{tmpListFile}\" -c copy \"{outputFile}\""
                            }
                        };

                        //execute FFMPEG
                        p.Start();

                        //don't continue until FFMPEG quits
                        p.WaitForExit();

                        //delete the merge list if it exists
                        if (File.Exists(tmpListFile))
                            File.Delete(tmpListFile);
                    }
                    else

                        //report error
                        ConsoleWriters.ConsoleWriteError(@"Concat remux failed; output file specified already exists");
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError($"Couldn't concat remux because {fileName} was not found");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"FFMPEG Concat remux error: {ex}");
            }
        }
    }
}