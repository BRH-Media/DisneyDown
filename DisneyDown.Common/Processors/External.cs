using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
                const string FILE_NAME = @"mp4decrypt.exe";

                if (File.Exists(FILE_NAME))
                {
                    if (!File.Exists(outputFile) || forceOverwrite)
                    {
                        //declare bento4 executable startup
                        var p = new Process
                        {
                            StartInfo =
                            {
                                FileName = FILE_NAME,
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
                        Console.WriteLine(@"Decrypt failed; output file specified already exists");
                }
                else
                    Console.WriteLine($"Couldn't decrypt because {FILE_NAME} was not found");
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($"Decryption error:\n\n{ex.Message}");
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
                const string FILE_NAME = @"ffmpeg.exe";

                if (File.Exists(FILE_NAME))
                {
                    if (!File.Exists(outputFile) || forceOverwrite)
                    {
                        //loop through each input file to build the string
                        var inputFileString = inputFiles.Aggregate(@"", (current, s) => current + $" -i \"{s}\"");

                        //declare FFMPEG executable startup
                        var p = new Process
                        {
                            StartInfo =
                            {
                                FileName = FILE_NAME,
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
                        Console.WriteLine(@"Remux failed; output file specified already exists");
                }
                else
                    Console.WriteLine($"Couldn't remux because {FILE_NAME} was not found");
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($"FFMPEG Remux error:\n\n{ex}");
            }
        }

        private static void WriteMergeList(string filePath, List<string> files)
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
                Console.WriteLine($@"Merge file write error: {ex.Message}");
            }
        }

        /// <summary>
        /// FFMPEG wrapper for remuxing audio and video files
        /// </summary>
        /// <param name="inputFiles"></param>
        /// <param name="outputFile"></param>
        /// <param name="forceOverwrite"></param>
        public static void DoConcatMux(List<string> inputFiles, string outputFile = @"content.mkv", bool forceOverwrite = false)
        {
            try
            {
                //concatenation list file
                var tmpListFile = $@"{Path.GetDirectoryName(outputFile)}\mergeDef";

                //FFMPEG executable to run
                const string FILE_NAME = @"ffmpeg.exe";

                //ensure FFMPEG exists
                if (File.Exists(FILE_NAME))
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
                                FileName = FILE_NAME,
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
                        Console.WriteLine(@"Concat remux failed; output file specified already exists");
                }
                else
                    Console.WriteLine($"Couldn't concat remux because {FILE_NAME} was not found");
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($"FFMPEG Concat remux error:\n\n{ex}");
            }
        }
    }
}