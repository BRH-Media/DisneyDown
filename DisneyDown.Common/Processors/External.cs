using System;
using System.Diagnostics;
using System.IO;

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
                const string fileName = @"mp4decrypt.exe";

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
                                Arguments = $"--key 1:{key} --show-progress {inputFile} {outputFile}"
                            }
                        };

                        //execute the decryptor
                        p.Start();

                        //don't continue until the decrypter quits
                        p.WaitForExit();
                    }
                }
                else
                    Console.WriteLine($"Couldn't decrypt because {fileName} was not found");
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
        /// <param name="audioFile"></param>
        /// <param name="videoFile"></param>
        /// <param name="outputFile"></param>
        /// <param name="forceOverwrite"></param>
        public static void DoMux(string audioFile, string videoFile, string outputFile = @"content.mkv", bool forceOverwrite = false)
        {
            try
            {
                const string fileName = @"ffmpeg.exe";

                if (File.Exists(fileName))
                {
                    if (!File.Exists(outputFile) || forceOverwrite)
                    {
                        //declare FFMPEG executable startup
                        var p = new Process
                        {
                            StartInfo =
                            {
                                FileName = fileName,
                                WindowStyle = ProcessWindowStyle.Hidden,
                                Arguments = $"-i {videoFile} -i {audioFile} -c copy {outputFile}"
                            }
                        };

                        //execute FFMPEG
                        p.Start();

                        //don't continue until FFMPEG quits
                        p.WaitForExit();
                    }
                }
                else
                    Console.WriteLine($"Couldn't remux because {fileName} was not found");
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($"FFMPEG Remux error:\n\n{ex}");
            }
        }
    }
}