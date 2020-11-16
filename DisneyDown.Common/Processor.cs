using DisneyDown.Common.Processors;
using DisneyDown.Common.Processors.Downloaders;
using DisneyDown.Common.Processors.Parsers;
using System;
using System.IO;

// ReSharper disable RedundantIfElseBlock
// ReSharper disable InconsistentNaming

namespace DisneyDown.Common
{
    public static class Processor
    {
        public static string ManifestURL { get; set; } = @"";
        public static string DecryptionKey { get; set; } = @"";
        public static string OutFileName { get; set; } = @"decryptedDisney.mkv";

        /// <summary>
        /// Verifies the existence of ffmpeg and bento4 mp4decrypt.
        /// </summary>
        /// <returns></returns>
        private static bool CheckRequiredExecutables() => File.Exists(@"ffmpeg.exe") && File.Exists(@"mp4decrypt.exe");

        /// <summary>
        /// Downloads and decrypts an audio stream; returns the decrypted file path
        /// </summary>
        /// <param name="masterPlaylist"></param>
        /// <param name="workingDir"></param>
        public static string StartAudio(string masterPlaylist, string workingDir)
        {
            //file names
            const string encryptedAudioFileName = @"audioEncrypted.bin";
            const string decryptedAudioFileName = @"audioDecrypted.mp4";

            //file paths
            var encryptedAudio = $@"{workingDir}\{encryptedAudioFileName}";
            var decryptedAudio = $@"{workingDir}\{decryptedAudioFileName}";

            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(masterPlaylist))
                {
                    //start measuring audio download time
                    Timers.StartTimer(Timers.AudioDownloadTimer);

                    //download audio where audioFile is the path of the saved data
                    var audioFile =
                        AudioDownloader.DownloadBestAudioFromMaster(
                            masterPlaylist,
                            ManifestURL,
                            encryptedAudio);

                    //stop measuring audio download time
                    Timers.StopTimer(Timers.AudioDownloadTimer);

                    //report progress
                    Console.WriteLine(@"Attempting decryption on audio");

                    //start measuring audio decrypt time
                    Timers.StartTimer(Timers.AudioDecryptTimer);

                    //decrypt audio stream
                    External.DoDecrypt(audioFile, decryptedAudio, DecryptionKey);

                    //stop measuring audio decrypt time
                    Timers.StopTimer(Timers.AudioDecryptTimer);

                    //report progress
                    Console.WriteLine(@"Audio decryption finished");

                    //return decrypted file path
                    return decryptedAudio;
                }
                else
                    Console.WriteLine(@"Audio processor failed; invalid master playlist");
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Audio process error: {ex.Message}");
            }

            //default
            return @"";
        }

        /// <summary>
        /// Downloads and decrypts a video stream; returns the decrypted file path
        /// </summary>
        /// <param name="masterPlaylist"></param>
        /// <param name="workingDir"></param>
        public static string StartVideo(string masterPlaylist, string workingDir)
        {
            //file names
            const string encryptedVideoFileName = @"videoEncrypted.bin";
            const string decryptedVideoFileName = @"videoDecrypted.mp4";

            //file paths
            var encryptedVideo = $@"{workingDir}\{encryptedVideoFileName}";
            var decryptedVideo = $@"{workingDir}\{decryptedVideoFileName}";

            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(masterPlaylist))
                {
                    //start measuring video download time
                    Timers.StartTimer(Timers.VideoDownloadTimer);

                    //download video where videoFile is the path of the saved data
                    var videoFile =
                        VideoDownloader.DownloadBestVideoFromMaster(
                            masterPlaylist,
                            ManifestURL,
                            encryptedVideo);

                    //stop measuring video download time
                    Timers.StopTimer(Timers.VideoDownloadTimer);

                    //report progress
                    Console.WriteLine(@"Attempting decryption on video");

                    //start measuring video decrypt time
                    Timers.StartTimer(Timers.VideoDecryptTimer);

                    //decrypt video stream
                    External.DoDecrypt(videoFile, decryptedVideo, DecryptionKey);

                    //stop measuring video decrypt time
                    Timers.StopTimer(Timers.VideoDecryptTimer);

                    //report progress
                    Console.WriteLine(@"Video decryption finished");

                    //return decrypted file path
                    return decryptedVideo;
                }
                else
                    Console.WriteLine(@"Video processor failed; invalid master playlist");
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Video process error: {ex.Message}");
            }

            //default
            return @"";
        }

        public static string StartProcessor()
        {
            try
            {
                //executable validation
                if (!CheckRequiredExecutables())
                    Console.WriteLine(
                        @"Process failed; required executable ffmpeg.exe and/or mp4decrypt.exe was not present.");
                else
                {
                    //validation
                    if (string.IsNullOrWhiteSpace(ManifestURL) || string.IsNullOrWhiteSpace(DecryptionKey))
                        Console.WriteLine(@"Process failed; one or more arguments were incorrect");
                    else
                    {
                        //key validation information
                        const int correctKeyLength = 32;
                        var actualKeyLength = DecryptionKey.Length;

                        //actual key validation
                        if (actualKeyLength != correctKeyLength)
                            Console.WriteLine(
                                $@"Process failed; key was of incorrect length. Expected length of {correctKeyLength} but got {actualKeyLength}.");
                        else
                        {
                            //ensure manifest URI is a valid URI and is a valid .m3u8 HLS manifest file
                            var masterUriValid = Uri.TryCreate(ManifestURL, UriKind.Absolute, out var uriResult)
                                                 && (uriResult.Scheme == Uri.UriSchemeHttp ||
                                                     uriResult.Scheme == Uri.UriSchemeHttps)
                                                 && (new FileInfo(uriResult.AbsolutePath).Extension.ToLower() == @".m3u8"
                                                        || new FileInfo(uriResult.AbsolutePath).Extension.ToLower() == @".m3u");

                            //only proceed if valid
                            if (!masterUriValid)
                                Console.WriteLine(@"Process failed; master playlist URL was invalid");
                            else
                            {
                                //report progress
                                Console.WriteLine(@"Downloading master playlist");

                                //download master
                                var masterPlaylist = ManifestParsers.DownloadManifest(ManifestURL);

                                //check conformity
                                if (ManifestParsers.ManifestValid(masterPlaylist))
                                {
                                    //directories for temporary storage
                                    const string baseOutputDir = @"output";
                                    const string baseWorkingDir = @"tmp";

                                    //unique hash for master manifest URL (MD5)
                                    var masterManifestHash = Md5Helper.CalculateMd5Hash(ManifestURL);

                                    //actual working directory
                                    var workingDir = $@"{baseWorkingDir}\{masterManifestHash}";
                                    var outputDir = $@"{baseOutputDir}\{masterManifestHash}";

                                    //output file path
                                    var outputFile = $@"{outputDir}\{OutFileName}";

                                    //ensure the final output directory exists
                                    if (!Directory.Exists(outputDir))
                                        Directory.CreateDirectory(outputDir);

                                    //ensure the temporary working directory exists
                                    if (!Directory.Exists(workingDir))
                                        Directory.CreateDirectory(workingDir);

                                    //do audio and video processes
                                    var decryptedVideo = StartVideo(masterPlaylist, workingDir);
                                    var decryptedAudio = StartAudio(masterPlaylist, workingDir);

                                    //report progress
                                    Console.WriteLine($"\nDecrypted video path: {decryptedVideo}");
                                    Console.WriteLine($"Decrypted audio path: {decryptedAudio}");
                                    Console.WriteLine($"Output path: {outputFile}\n");

                                    //report progress
                                    Console.WriteLine("Attempting stream remux");

                                    //attempt mux audio and video together (only if the decryption succeeded)
                                    if (File.Exists(decryptedVideo) && File.Exists(decryptedAudio))
                                    {
                                        //start measuring remux time
                                        Timers.StartTimer(Timers.RemuxTimer);

                                        //execute FFMPEG
                                        External.DoMux(decryptedAudio, decryptedVideo, outputFile);

                                        //stop measuring remux time
                                        Timers.StopTimer(Timers.RemuxTimer);

                                        //report progress
                                        Console.WriteLine(@"Remux process completed");

                                        //return output file
                                        return outputFile;
                                    }
                                    else
                                        Console.WriteLine(
                                            @"FFMPEG Mux failed because one or more decrypted files were not available; check your key and try again.");
                                }
                                else
                                    Console.WriteLine(
                                        @"Process failed; master playlist does not conform and is therefore invalid.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Process error: {ex.Message}");
            }

            //default
            return @"";
        }
    }
}