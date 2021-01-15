using DisneyDown.Common.Globals;
using DisneyDown.Common.Parsers;
using DisneyDown.Common.Processors.Downloaders.Audio;
using DisneyDown.Common.Processors.Downloaders.Subtitles;
using DisneyDown.Common.Processors.Downloaders.Video;
using DisneyDown.Common.Security;
using DisneyDown.Common.Util.Diagnostics;
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
                    var subtitlesDirectory =
                        MainSubtitlesDownloader.DownloadSubtitlesFromMaster(
                            masterPlaylist,
                            Strings.ManifestUrl,
                            subtitleMergeFile);

                    //the downloader will return the merged file if it already exists; make sure to check for this
                    if (File.Exists(subtitlesDirectory))
                    {
                        //stop measuring subtitle parse and merge time
                        Timers.StopTimer(Timers.Generic.SubtitlesParseTimer);

                        //return the already existing file and do not attempt extra processing
                        return subtitleMergeFile;
                    }

                    //report progress
                    Console.WriteLine(@"Attempting subtitles parse and merge");

                    //start measuring subtitles parse and merge time
                    Timers.StartTimer(Timers.Generic.SubtitlesParseTimer);

                    //total lines
                    var subtitles = new List<string>();

                    //go through each subtitle file in the directory
                    foreach (var f in Directory.GetFiles(subtitlesDirectory))
                    {
                        //extension of the file
                        var ext = Path.GetExtension(f);

                        //make sure it's WebVTT
                        if (ext == @".vtt")

                            //append
                            subtitles.AddRange(File.ReadAllLines(f));
                    }

                    //check if there are lines
                    if (subtitles.Count > 0)
                    {
                        //conversion
                        var convertedSubs = SubtitleProcessor.ConvertToSrt(subtitles);

                        //export to the merge file
                        File.WriteAllLines(subtitleMergeFile, convertedSubs);
                    }

                    //stop measuring subtitle parse and merge time
                    Timers.StopTimer(Timers.Generic.SubtitlesParseTimer);

                    //report progress
                    Console.WriteLine(@"Subtitles processing completed");

                    //return merged subtitles file path
                    return subtitleMergeFile;
                }
                else

                    //report error
                    Console.WriteLine(@"Subtitles processor failed; invalid master playlist");
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($@"Subtitles process error: {ex.Message}");
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
                    //download audio where audioFile is the path of the saved data
                    var audioFile =
                        MainAudioDownloader.DownloadBestAudioFromMaster(
                            masterPlaylist,
                            Strings.ManifestUrl,
                            encryptedAudio);

                    //report progress
                    Console.WriteLine(@"Attempting decryption on audio");

                    //start measuring audio decrypt time
                    Timers.StartTimer(Timers.Generic.AudioDecryptTimer);

                    //decrypt audio stream
                    External.DoDecrypt(audioFile, decryptedAudio, Strings.DecryptionKey);

                    //stop measuring audio decrypt time
                    Timers.StopTimer(Timers.Generic.AudioDecryptTimer);

                    //start measuring bumper audio decrypt time
                    Timers.StartTimer(Timers.Bumper.BumperAudioDecryptTimer);

                    //decrypt bumper (if enabled)
                    if (Args.DownloadBumperEnabled)
                        External.DoDecrypt(encryptedBumper, decryptedBumper, Strings.DecryptionKey);

                    //stop measuring bumper audio decrypt time
                    Timers.StopTimer(Timers.Bumper.BumperAudioDecryptTimer);

                    //report progress
                    Console.WriteLine(@"Audio decryption finished");

                    //return decrypted file path
                    return decryptedAudio;
                }
                else

                    //report error
                    Console.WriteLine(@"Audio processor failed; invalid master playlist");
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($@"Audio process error: {ex.Message}");
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
                    Console.WriteLine(@"Attempting decryption on video");

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
                    Console.WriteLine(@"Video decryption finished");

                    //return decrypted file path
                    return decryptedVideo;
                }
                else

                    //report error
                    Console.WriteLine(@"Video processor failed; invalid master playlist");
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($@"Video process error: {ex.Message}");
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
                    Console.WriteLine(
                        @"Process failed; required executable ffmpeg.exe and/or mp4decrypt.exe was not present.");
                else
                {
                    //validation
                    if (string.IsNullOrWhiteSpace(Strings.ManifestUrl) || string.IsNullOrWhiteSpace(Strings.DecryptionKey))

                        //report error
                        Console.WriteLine(@"Process failed; one or more arguments were incorrect");
                    else
                    {
                        //the length of a valid hex Widevine key
                        const int correctKeyLength = 32;

                        //the length of the hex Widevine key provided by the user
                        var actualKeyLength = Strings.DecryptionKey.Length;

                        //actual key validation
                        if (actualKeyLength != correctKeyLength)

                            //report error
                            Console.WriteLine(
                                $@"Process failed; key was of incorrect length. Expected length of {correctKeyLength} but got {actualKeyLength}.");
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
                                Console.WriteLine(@"Process failed; master playlist URL was invalid");
                            else
                            {
                                //report progress
                                Console.WriteLine(@"Downloading master playlist");

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
                                    Console.WriteLine($"\nDecrypted video path: {decryptedVideo}");
                                    Console.WriteLine($"Decrypted audio path: {decryptedAudio}");
                                    Console.WriteLine($"Output path: {outputFile}\n");

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
                                        Console.WriteLine("Attempting main stream remux");

                                        //execute FFMPEG
                                        External.DoMux(muxInput, outputFile);

                                        //report progress
                                        Console.WriteLine("Main stream remux process completed");

                                        //mux the bumper if allowed
                                        if (Args.DownloadBumperEnabled)
                                        {
                                            if (AllExistInList(muxBumperInput))
                                            {
                                                //report progress
                                                Console.WriteLine("Attempting bumper stream remux");

                                                //start measuring bumper remux time
                                                Timers.StartTimer(Timers.Bumper.BumperRemuxTimer);

                                                //combine bumper audio and video
                                                External.DoMux(muxBumperInput, decryptedBumper);

                                                //stop measuring bumper remux time
                                                Timers.StopTimer(Timers.Bumper.BumperRemuxTimer);

                                                //report progress
                                                Console.WriteLine("Bumper stream remux process completed");

                                                //combine the bumper and main content (if bumper mux succeeded)
                                                if (File.Exists(decryptedBumper))
                                                {
                                                    //report progress
                                                    Console.WriteLine("Attempting main stream and bumper concatenation");

                                                    //start measuring bumper concat time
                                                    Timers.StartTimer(Timers.Bumper.BumperConcatTimer);

                                                    //perform concat operation
                                                    External.DoConcatMux(new List<string> { decryptedBumper, outputFile },
                                                        decryptedMerged);

                                                    //stop measuring bumper concat time
                                                    Timers.StopTimer(Timers.Bumper.BumperConcatTimer);

                                                    //report progress
                                                    Console.WriteLine("Main stream and bumper concatenation process completed");
                                                }
                                                else

                                                    //report error
                                                    Console.WriteLine(
                                                        @"FFMPEG bumper concat mux failed because the decrypted bumper was not available; check your key and try again.");
                                            }
                                            else

                                                //report error
                                                Console.WriteLine(
                                                    @"FFMPEG bumper mux failed because one or more decrypted files were not available; check your key and try again.");
                                        }

                                        //stop measuring remux time
                                        Timers.StopTimer(Timers.Generic.RemuxTimer);

                                        //report progress
                                        Console.WriteLine(@"Overall remux process completed");

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
                                        Console.WriteLine(
                                            @"FFMPEG main stream mux failed because one or more decrypted files were not available; check your key and try again.");
                                }
                                else

                                    //report error
                                    Console.WriteLine(
                                        @"Process failed; master playlist does not conform and is therefore invalid.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($@"Main process error: {ex.Message}");
            }

            //default
            return @"";
        }
    }
}