using System;
using System.Diagnostics;
using System.IO;

namespace DisneyDown.Common.Processors
{
    public static class External
    {
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