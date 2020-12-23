using DisneyDown.Common.Processors.Downloaders.Audio;
using DisneyDown.Common.Processors.Downloaders.Video;
using DisneyDown.Common.Processors.Parsers;
using DisneyDown.Common.Security;
using DisneyDown.Common.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// ReSharper disable RedundantIfElseBlock
// ReSharper disable InconsistentNaming

namespace DisneyDown.Common.Processors
{
    public static class MainProcessor
    {
        /// <summary>
        /// Downloads and decrypts an audio stream; returns the decrypted file path
        /// </summary>
        /// <param name="masterPlaylist"></param>
        /// <param name="workingDir"></param>
        public static string StartAudio(string masterPlaylist, string workingDir)
        {
            //main file names
            const string encryptedAudioFileName = @"audioEncrypted.bin";
            const string decryptedAudioFileName = @"audioDecrypted.mp4";

            //Disney+ intro (bumper) file names
            const string encryptedBumperFileName = @"bumperAudioEncrypted.bin";
            const string decryptedBumperFileName = @"bumperAudioDecrypted.mp4";

            //main file paths
            var encryptedAudio = $@"{workingDir}\{encryptedAudioFileName}";
            var decryptedAudio = $@"{workingDir}\{decryptedAudioFileName}";

            //Disney+ intro (bumper) file paths
            var encryptedBumper = $@"{workingDir}\{encryptedBumperFileName}";
            var decryptedBumper = $@"{workingDir}\{decryptedBumperFileName}";

            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(masterPlaylist))
                {
                    //start measuring audio download time
                    Timers.StartTimer(Timers.AudioDownloadTimer);

                    //download audio where audioFile is the path of the saved data
                    var audioFile =
                        MainAudioDownloader.DownloadBestAudioFromMaster(
                            masterPlaylist,
                            Globals.ManifestUrl,
                            encryptedAudio);

                    //stop measuring audio download time
                    Timers.StopTimer(Timers.AudioDownloadTimer);

                    //report progress
                    Console.WriteLine(@"Attempting decryption on audio");

                    //start measuring audio decrypt time
                    Timers.StartTimer(Timers.AudioDecryptTimer);

                    //decrypt audio stream
                    External.DoDecrypt(audioFile, decryptedAudio, Globals.DecryptionKey);

                    //decrypt bumper (if enabled)
                    if (Globals.DownloadBumperEnabled)
                        External.DoDecrypt(encryptedBumper, decryptedBumper, Globals.DecryptionKey);

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
            //main file names
            const string encryptedVideoFileName = @"videoEncrypted.bin";
            const string decryptedVideoFileName = @"videoDecrypted.mp4";

            //Disney+ intro (bumper) file names
            const string encryptedBumperFileName = @"bumperVideoEncrypted.bin";
            const string decryptedBumperFileName = @"bumperVideoDecrypted.mp4";

            //main file paths
            var encryptedVideo = $@"{workingDir}\{encryptedVideoFileName}";
            var decryptedVideo = $@"{workingDir}\{decryptedVideoFileName}";

            //Disney+ intro (bumper) file paths
            var encryptedBumper = $@"{workingDir}\{encryptedBumperFileName}";
            var decryptedBumper = $@"{workingDir}\{decryptedBumperFileName}";

            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(masterPlaylist))
                {
                    //start measuring video download time
                    Timers.StartTimer(Timers.VideoDownloadTimer);

                    //download video where videoFile is the path of the saved data
                    var videoFile =
                        MainVideoDownloader.DownloadBestVideoFromMaster(
                            masterPlaylist,
                            Globals.ManifestUrl,
                            encryptedVideo);

                    //stop measuring video download time
                    Timers.StopTimer(Timers.VideoDownloadTimer);

                    //report progress
                    Console.WriteLine(@"Attempting decryption on video");

                    //start measuring video decrypt time
                    Timers.StartTimer(Timers.VideoDecryptTimer);

                    //decrypt video stream
                    External.DoDecrypt(videoFile, decryptedVideo, Globals.DecryptionKey);

                    //decrypt bumper (if enabled)
                    if (Globals.DownloadBumperEnabled)
                        External.DoDecrypt(encryptedBumper, decryptedBumper, Globals.DecryptionKey);

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

        private static bool AllExistInList(IEnumerable<string> inputFiles)
        {
            try
            {
                return inputFiles.All(File.Exists);
            }
            catch
            {
                //nothing
            }

            //default
            return false;
        }

        public static string StartProcessor()
        {
            try
            {
                //executable validation
                if (!Globals.CheckRequiredExecutables())
                    Console.WriteLine(
                        @"Process failed; required executable ffmpeg.exe and/or mp4decrypt.exe was not present.");
                else
                {
                    //validation
                    if (string.IsNullOrWhiteSpace(Globals.ManifestUrl) || string.IsNullOrWhiteSpace(Globals.DecryptionKey))
                        Console.WriteLine(@"Process failed; one or more arguments were incorrect");
                    else
                    {
                        //key validation information
                        const int correctKeyLength = 32;
                        var actualKeyLength = Globals.DecryptionKey.Length;

                        //actual key validation
                        if (actualKeyLength != correctKeyLength)
                            Console.WriteLine(
                                $@"Process failed; key was of incorrect length. Expected length of {correctKeyLength} but got {actualKeyLength}.");
                        else
                        {
                            //ensure manifest URI is a valid URI and is a valid .m3u8 HLS manifest file
                            var masterUriValid = Uri.TryCreate(Globals.ManifestUrl, UriKind.Absolute, out var uriResult)
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
                                var masterPlaylist = ManifestParsers.DownloadManifest(Globals.ManifestUrl);

                                //check conformity
                                if (ManifestParsers.ManifestValid(masterPlaylist))
                                {
                                    //directories for temporary storage
                                    const string baseOutputDir = @"output";
                                    const string baseWorkingDir = @"tmp";

                                    //unique hash for master manifest URL (MD5)
                                    var masterManifestHash = Md5Helper.CalculateMd5Hash(Globals.ManifestUrl);

                                    //actual working directory
                                    var workingDir = $@"{baseWorkingDir}\{masterManifestHash}";
                                    var outputDir = $@"{baseOutputDir}\{masterManifestHash}";

                                    //output file path
                                    var outputFile = $@"{outputDir}\{Globals.OutFileName}";

                                    //ensure the final output directory exists
                                    if (!Directory.Exists(outputDir))
                                        Directory.CreateDirectory(outputDir);

                                    //ensure the temporary working directory exists
                                    if (!Directory.Exists(workingDir))
                                        Directory.CreateDirectory(workingDir);

                                    //decrypted outputs
                                    var decryptedAudio = @"";
                                    var decryptedVideo = @"";

                                    //do audio and video processes
                                    if (!Globals.AudioOnlyMode)
                                        decryptedVideo = StartVideo(masterPlaylist, workingDir);
                                    if (!Globals.VideoOnlyMode)
                                        decryptedAudio = StartAudio(masterPlaylist, workingDir);

                                    //decrypted video
                                    var decryptedBumperVideo = $@"{Path.GetDirectoryName(decryptedVideo)}\bumperVideoDecrypted.mp4";

                                    //decrypted audio
                                    var decryptedBumperAudio = $@"{Path.GetDirectoryName(decryptedAudio)}\bumperAudioDecrypted.mp4";

                                    //decrypted bumper to be saved
                                    var decryptedBumper = $@"{outputDir}\decryptedBumper.mkv";
                                    var decryptedMerged = $@"{outputDir}\decryptedMerged.mkv";

                                    //report progress
                                    Console.WriteLine($"\nDecrypted video path: {decryptedVideo}");
                                    Console.WriteLine($"Decrypted audio path: {decryptedAudio}");
                                    Console.WriteLine($"Output path: {outputFile}\n");

                                    //assemble input files list
                                    var muxInput = new List<string>();
                                    var muxBumperInput = new List<string> { decryptedBumperAudio, decryptedBumperVideo };

                                    //add the files to remux
                                    //audio
                                    if (!string.IsNullOrWhiteSpace(decryptedAudio))
                                        muxInput.Add(decryptedAudio);

                                    //video
                                    if (!string.IsNullOrWhiteSpace(decryptedVideo))
                                        muxInput.Add(decryptedVideo);

                                    //attempt mux audio and video together (only if the decryption succeeded)
                                    if (AllExistInList(muxInput))
                                    {
                                        //start measuring remux time
                                        Timers.StartTimer(Timers.RemuxTimer);

                                        //report progress
                                        Console.WriteLine("Attempting main stream remux");

                                        //execute FFMPEG
                                        External.DoMux(muxInput, outputFile);

                                        //report progress
                                        Console.WriteLine("Main stream remux process completed");

                                        //mux the bumper if allowed
                                        if (Globals.DownloadBumperEnabled)
                                        {
                                            if (AllExistInList(muxBumperInput))
                                            {
                                                //report progress
                                                Console.WriteLine("Attempting bumper stream remux");

                                                //combine bumper audio and video
                                                External.DoMux(muxBumperInput, decryptedBumper);

                                                //report progress
                                                Console.WriteLine("Bumper stream remux process completed");

                                                //combine the bumper and main content (if bumper mux succeeded)
                                                if (File.Exists(decryptedBumper))
                                                {
                                                    //report progress
                                                    Console.WriteLine("Attempting main stream and bumper concatenation");

                                                    //perform concat operation
                                                    External.DoConcatMux(new List<string> { decryptedBumper, outputFile },
                                                        decryptedMerged);

                                                    //report progress
                                                    Console.WriteLine("Main stream and bumper concatenation process completed");
                                                }
                                                else
                                                    Console.WriteLine(
                                                        @"FFMPEG bumper concat mux failed because the decrypted bumper was not available; check your key and try again.");
                                            }
                                            else
                                                Console.WriteLine(
                                                    @"FFMPEG bumper mux failed because one or more decrypted files were not available; check your key and try again.");
                                        }

                                        //stop measuring remux time
                                        Timers.StopTimer(Timers.RemuxTimer);

                                        //report progress
                                        Console.WriteLine(@"Overall remux process completed");

                                        //return output file
                                        return outputFile;
                                    }
                                    else
                                        Console.WriteLine(
                                            @"FFMPEG main stream mux failed because one or more decrypted files were not available; check your key and try again.");
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