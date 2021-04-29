using DisneyDown.Common.Globals;
using DisneyDown.Common.Parsers;
using DisneyDown.Common.Processors.Downloaders.Audio;
using DisneyDown.Common.Processors.Downloaders.Subtitles;
using DisneyDown.Common.Processors.Downloaders.Video;
using DisneyDown.Common.Security;
using DisneyDown.Common.Util.Diagnostics;
using DisneyDown.Common.Util.Kit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// ReSharper disable UnusedVariable
// ReSharper disable UnusedMember.Global
// ReSharper disable LocalizableElement
// ReSharper disable RedundantIfElseBlock
// ReSharper disable InconsistentNaming

namespace DisneyDown.Common.Processors
{
    public static class MainProcessor
    {
        /// <summary>
        /// Downloads a subtitle stream; returns the merged subtitle file path
        /// </summary>
        /// <param name="masterPlaylist"></param>
        /// <param name="workingDir"></param>
        public static string StartSubtitles(string masterPlaylist, string workingDir)
        {
            //main file name
            const string subtitleMergeFileName = @"subtitles.srt";

            //main file path
            var subtitleMergeFile = $@"{workingDir}\{subtitleMergeFileName}";

            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(masterPlaylist))
                {
                    //download subtitles
                    var subtitlesFile =
                        MainSubtitlesDownloader.DownloadSubtitlesFromMaster(
                            masterPlaylist,
                            Strings.ManifestUrl,
                            subtitleMergeFile);

                    //the downloader will return the merged file if it already exists; make sure to check for this
                    if (File.Exists(subtitlesFile))
                    {
                        //stop measuring subtitle parse and merge time
                        Timers.StopTimer(Timers.Generic.SubtitlesParseTimer);

                        //return the already existing file and do not attempt extra processing
                        return subtitleMergeFile;
                    }

                    //report progress
                    ConsoleWriters.WriteLine(@"[i] Subtitles processing completed", ConsoleColor.Green);

                    //return merged subtitles file path
                    return subtitleMergeFile;
                }
                else

                    //report error
                    ConsoleWriters.WriteLine(@"[!] Subtitles processor failed; invalid master playlist", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.WriteLine($@"[!] Subtitles process error: {ex.Message}", ConsoleColor.Red);
            }

            //stop measuring subtitle parse and merge time
            Timers.StopTimer(Timers.Generic.SubtitlesParseTimer);

            //default
            return @"";
        }

        /// <summary>
        /// Downloads and decrypts an audio stream; returns the decrypted file path
        /// </summary>
        /// <param name="masterPlaylist"></param>
        /// <param name="workingDir"></param>
        public static string StartAudio(string masterPlaylist, string workingDir)
        {
            //main file names
            const string decryptedAudioFileName = @"audioDecrypted.mp4";

            //main file paths
            var decryptedAudio = $@"{workingDir}\{decryptedAudioFileName}";

            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(masterPlaylist))
                {
                    //download audio where audioFile is the path of the saved data
                    var audioFile =
                        MainAudioDownloader.DownloadBestAudioFromMaster(
                            masterPlaylist,
                            Strings.ManifestUrl,
                            decryptedAudio);

                    //return decrypted file path
                    return decryptedAudio;
                }
                else

                    //report error
                    ConsoleWriters.WriteLine(@"[!] Audio processor failed; invalid master playlist", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.WriteLine($@"[!] Audio process error: {ex.Message}", ConsoleColor.Red);
            }

            //stop measuring audio decrypt time
            Timers.StopTimer(Timers.Generic.AudioDecryptTimer);

            //stop measuring bumper audio decrypt time
            Timers.StopTimer(Timers.Bumper.BumperAudioDecryptTimer);

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
                    //download video where videoFile is the path of the saved data
                    var videoFile =
                        MainVideoDownloader.DownloadBestVideoFromMaster(
                            masterPlaylist,
                            Strings.ManifestUrl,
                            encryptedVideo);

                    //report progress
                    ConsoleWriters.WriteLine(@"[i] Attempting decryption on video", ConsoleColor.Cyan);

                    //start measuring video decrypt time
                    Timers.StartTimer(Timers.Generic.VideoDecryptTimer);

                    //decrypt video stream
                    External.DoDecrypt(videoFile, decryptedVideo, Strings.DecryptionKey);

                    //stop measuring video decrypt time
                    Timers.StopTimer(Timers.Generic.VideoDecryptTimer);

                    //start measuring bumper video decrypt time
                    Timers.StartTimer(Timers.Bumper.BumperVideoDecryptTimer);

                    //decrypt bumper (if enabled)
                    if (Args.DownloadBumperEnabled)
                        External.DoDecrypt(encryptedBumper, decryptedBumper, Strings.DecryptionKey);

                    //start measuring bumper video decrypt time
                    Timers.StopTimer(Timers.Bumper.BumperVideoDecryptTimer);

                    //report progress
                    ConsoleWriters.WriteLine(@"[i] Video decryption finished", ConsoleColor.Green);

                    //return decrypted file path
                    return decryptedVideo;
                }
                else

                    //report error
                    ConsoleWriters.WriteLine(@"[!] Video processor failed; invalid master playlist", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.WriteLine($@"[!] Video process error: {ex.Message}", ConsoleColor.Red);
            }

            //stop measuring video decrypt time
            Timers.StopTimer(Timers.Generic.VideoDecryptTimer);

            //start measuring bumper video decrypt time
            Timers.StopTimer(Timers.Bumper.BumperVideoDecryptTimer);

            //default
            return @"";
        }

        private static bool AllExistInList(IEnumerable<string> inputFiles)
        {
            try
            {
                //return true only if every single file in the list exists
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
                if (!Args.CheckRequiredExecutables)

                    //report error
                    ConsoleWriters.WriteLine(
                        @"[!] Process failed; required executable ffmpeg.exe and/or mp4decrypt.exe was not present.", ConsoleColor.Red);
                else
                {
                    //validation
                    if (string.IsNullOrWhiteSpace(Strings.ManifestUrl) || string.IsNullOrWhiteSpace(Strings.DecryptionKey))

                        //report error
                        ConsoleWriters.WriteLine(@"[!] Process failed; one or more arguments were incorrect", ConsoleColor.Red);
                    else
                    {
                        //the length of a valid hex Widevine key
                        const int correctKeyLength = 32;

                        //the length of the hex Widevine key provided by the user
                        var actualKeyLength = Strings.DecryptionKey.Length;

                        //actual key validation
                        if (actualKeyLength != correctKeyLength)

                            //report error
                            ConsoleWriters.WriteLine(
                                $"[!] Process failed; key was of incorrect length. Expected length of " +
                                $"{correctKeyLength} but got {actualKeyLength}.", ConsoleColor.Red);
                        else
                        {
                            //ensure manifest URI is a valid URI and is a valid HLS manifest file
                            var masterUriValid =

                                //attempt to create a URI object from the provided master manifest URL
                                Uri.TryCreate(Strings.ManifestUrl, UriKind.Absolute, out var uriResult)

                                                 //can be either HTTP or HTTPS; no other protocols are allowed
                                                 && (uriResult.Scheme == Uri.UriSchemeHttp ||
                                                     uriResult.Scheme == Uri.UriSchemeHttps)

                                                 //can be either a .m3u8 file or a .m3u file; others are not valid HLS manifests
                                                 && (new FileInfo(uriResult.AbsolutePath).Extension.ToLower() == @".m3u8"
                                                        || new FileInfo(uriResult.AbsolutePath).Extension.ToLower() == @".m3u");

                            //only proceed if valid
                            if (!masterUriValid)

                                //report error
                                ConsoleWriters.WriteLine(@"[!] Process failed; master playlist URL was invalid", ConsoleColor.Red);
                            else
                            {
                                //report progress
                                ConsoleWriters.WriteLine(@"[i] Downloading master playlist", ConsoleColor.Cyan);

                                //download master
                                var masterPlaylist = ManifestParsers.DownloadManifest(Strings.ManifestUrl);

                                //check conformity
                                if (ManifestParsers.ManifestValid(masterPlaylist))
                                {
                                    //directories for temporary storage
                                    const string baseOutputDir = @"output";
                                    const string baseWorkingDir = @"tmp";

                                    //unique hash for master manifest URL (MD5)
                                    var masterManifestHash = Md5Helper.CalculateMd5Hash(Strings.ManifestUrl);

                                    //actual working directory
                                    var workingDir = $@"{baseWorkingDir}\{masterManifestHash}";
                                    var outputDir = $@"{baseOutputDir}\{masterManifestHash}";

                                    //output file path
                                    var outputFile = $@"{outputDir}\{Strings.OutFileName}";

                                    //ensure the final output directory exists
                                    if (!Directory.Exists(outputDir))
                                        Directory.CreateDirectory(outputDir);

                                    //ensure the temporary working directory exists
                                    if (!Directory.Exists(workingDir))
                                        Directory.CreateDirectory(workingDir);

                                    //decrypted outputs
                                    var decryptedAudio = @"";
                                    var decryptedVideo = @"";

                                    //subtitle file
                                    var mergedSubtitles = @"";

                                    //download, parse and merge subtitles
                                    if (!Args.VideoOnlyMode)
                                        mergedSubtitles = StartSubtitles(masterPlaylist, workingDir);

                                    //download and decrypt audio
                                    if (!Args.VideoOnlyMode)
                                        decryptedAudio = StartAudio(masterPlaylist, workingDir);

                                    //download and decrypt video
                                    if (!Args.AudioOnlyMode)
                                        decryptedVideo = StartVideo(masterPlaylist, workingDir);

                                    //decrypted video
                                    var decryptedBumperVideo = $@"{Path.GetDirectoryName(decryptedVideo)}\bumperVideoDecrypted.mp4";

                                    //decrypted audio
                                    var decryptedBumperAudio = $@"{Path.GetDirectoryName(decryptedAudio)}\bumperAudioDecrypted.mp4";

                                    //decrypted bumper to be saved
                                    var decryptedBumper = $@"{outputDir}\decryptedBumper.mkv";
                                    var decryptedMerged = $@"{outputDir}\decryptedMerged.mkv";

                                    //report progress
                                    ConsoleWriters.WriteLine($"\n[i] Decrypted video path: {decryptedVideo}", ConsoleColor.Cyan);
                                    ConsoleWriters.WriteLine($"[i] Decrypted audio path: {decryptedAudio}", ConsoleColor.Cyan);
                                    ConsoleWriters.WriteLine($"[i] Output path: {outputFile}\n", ConsoleColor.Cyan);

                                    //FFMPEG mux inputs for the main output file
                                    var muxInput = new List<string>();

                                    //FFMPEG mux inputs for the Disney+ intro (bumper)
                                    var muxBumperInput = new List<string> { decryptedBumperAudio, decryptedBumperVideo };

                                    //add the files to remux
                                    //~~~~~
                                    //audio
                                    if (!string.IsNullOrWhiteSpace(decryptedAudio))
                                        muxInput.Add(decryptedAudio);

                                    //video
                                    if (!string.IsNullOrWhiteSpace(decryptedVideo))
                                        muxInput.Add(decryptedVideo);

                                    //subtitles
                                    if (!string.IsNullOrWhiteSpace(mergedSubtitles))

                                        //don't add subtitles if they don't exist; this avoids the later check
                                        //that will crash the main application processor
                                        if (File.Exists(mergedSubtitles))
                                            muxInput.Add(mergedSubtitles);

                                    //attempt mux audio and video together (only if the decryption succeeded)
                                    if (AllExistInList(muxInput))
                                    {
                                        //start measuring remux time
                                        Timers.StartTimer(Timers.Generic.RemuxTimer);

                                        //report progress
                                        ConsoleWriters.WriteLine("[i] Attempting main stream remux", ConsoleColor.Cyan);

                                        //execute FFMPEG
                                        External.DoMux(muxInput, outputFile);

                                        //report progress
                                        ConsoleWriters.WriteLine("[i] Main stream remux process completed", ConsoleColor.Green);

                                        //mux the bumper if allowed
                                        if (Args.DownloadBumperEnabled)
                                        {
                                            if (AllExistInList(muxBumperInput))
                                            {
                                                //report progress
                                                ConsoleWriters.WriteLine("[i] Attempting bumper stream remux", ConsoleColor.Cyan);

                                                //start measuring bumper remux time
                                                Timers.StartTimer(Timers.Bumper.BumperRemuxTimer);

                                                //combine bumper audio and video
                                                External.DoMux(muxBumperInput, decryptedBumper);

                                                //stop measuring bumper remux time
                                                Timers.StopTimer(Timers.Bumper.BumperRemuxTimer);

                                                //report progress
                                                ConsoleWriters.WriteLine("[i] Bumper stream remux process completed", ConsoleColor.Green);

                                                //combine the bumper and main content (if bumper mux succeeded)
                                                if (File.Exists(decryptedBumper))
                                                {
                                                    //report progress
                                                    ConsoleWriters.WriteLine("[i] Attempting main stream and bumper concatenation", ConsoleColor.Cyan);

                                                    //start measuring bumper concat time
                                                    Timers.StartTimer(Timers.Bumper.BumperConcatTimer);

                                                    //perform concat operation
                                                    External.DoConcatMux(new List<string> { decryptedBumper, outputFile },
                                                        decryptedMerged);

                                                    //stop measuring bumper concat time
                                                    Timers.StopTimer(Timers.Bumper.BumperConcatTimer);

                                                    //report progress
                                                    ConsoleWriters.WriteLine("[i] Main stream and bumper concatenation process completed",
                                                        ConsoleColor.Green);
                                                }
                                                else

                                                    //report error
                                                    ConsoleWriters.WriteLine(
                                                        "[!] FFMPEG bumper concat mux failed because the decrypted bumper was not available; " +
                                                        "check your key and try again.", ConsoleColor.Red);
                                            }
                                            else

                                                //report error
                                                ConsoleWriters.WriteLine(
                                                    "[!] FFMPEG bumper mux failed because one or more decrypted files were not available; " +
                                                    "check your key and try again.", ConsoleColor.Red);
                                        }

                                        //stop measuring remux time
                                        Timers.StopTimer(Timers.Generic.RemuxTimer);

                                        //report progress
                                        ConsoleWriters.WriteLine(@"[i] Overall remux process completed", ConsoleColor.Green);

                                        //return output file
                                        return !Args.DownloadBumperEnabled

                                            //downloading the Disney+ intro was disabled; use the normal output file
                                            ? outputFile

                                            //downloading the Disney+ was enabled; check if the merged file exists
                                            : File.Exists(decryptedMerged)

                                                //the merge file exists; report it instead
                                                ? decryptedMerged

                                                //the merge file does not exist; report the normal output file
                                                : outputFile;
                                    }
                                    else

                                        //report error
                                        ConsoleWriters.WriteLine(
                                            "[!] FFMPEG main stream mux failed because one or more decrypted files were not available; " +
                                            "check your key and try again.", ConsoleColor.Red);
                                }
                                else

                                    //report error
                                    ConsoleWriters.WriteLine(
                                        @"[!] Process failed; master playlist does not conform and is therefore invalid.", ConsoleColor.Red);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.WriteLine($@"[!] Main process error: {ex.Message}", ConsoleColor.Red);
            }

            //default
            return @"";
        }
    }
}