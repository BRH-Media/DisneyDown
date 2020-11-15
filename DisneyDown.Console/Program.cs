using DisneyDown.Common;
using DisneyDown.Common.Processors;
using DisneyDown.Common.Processors.Downloaders;
using DisneyDown.Common.Processors.Parsers;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

// ReSharper disable InconsistentNaming

namespace DisneyDown.Console
{
    /// <summary>
    /// DisneyDown.Console main program code
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Name of the the executable (used in-case the user renames it)
        /// </summary>
        public static string ExecutableName => Path.GetFileName(Assembly.GetEntryAssembly()?.Location);

        /// <summary>
        /// Global storage for the Widevine decryption key
        /// </summary>
        public static string DecryptionKey { get; set; } = @"";

        /// <summary>
        /// Global storage for the Disney+ Manifest URL
        /// </summary>
        public static string ManifestURL { get; set; } = @"";

        /// <summary>
        /// Global storage for the remuxed file name
        /// </summary>
        public static string OutFileName { get; set; } = @"decryptedDisney.mkv";

        /// <summary>
        /// Prints the 'help' text
        /// </summary>
        private static void PrintUsage()
        {
            System.Console.WriteLine($@"usage: {ExecutableName} widevine_hex_key master_manifest_URL [output_file_name] [-t]");
            System.Console.WriteLine(@" options:");
            System.Console.WriteLine(@"  widevine_hex_key    - 32 character content decryption key");
            System.Console.WriteLine(@"  master_manifest_URL - m3u8 master manifest URL; do not input a locally available manifest");
            System.Console.WriteLine(@"  output_file_name    - Name of the remuxed file in the .\output folder");
            System.Console.WriteLine(@"  -t                  - Enables execution timing; reports how long each operation took");
        }

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            //main execution timer start
            Timers.StartTimer(Timers.ExecutionTimer);

            //verify arguments
            if (args.Length < 2
                || args.Contains(@"-?")
                || args.Contains(@"-help")
                || args.Contains(@"-h"))

                //help information
                PrintUsage();
            else
            {
                //set required globals
                DecryptionKey = args[0];
                ManifestURL = args[1];

                //optional output file name is the third parameter
                if (args.Length > 2)
                    OutFileName = args[2];

                //begin
                StartProcessor();
            }

            //main execution timer stop
            Timers.StopTimer(Timers.ExecutionTimer);

            //line break for timings
            if (Timers.TimersEnabled)
                System.Console.WriteLine();

            //report all diagnostics timings
            Timers.ReportTimers();

            //report finality
            System.Console.WriteLine("\nDone!");
        }

        /// <summary>
        /// Verifies the existence of ffmpeg and bento4 mp4decrypt.
        /// </summary>
        /// <returns></returns>
        private static bool CheckRequiredExecutables() => File.Exists(@"ffmpeg.exe") && File.Exists(@"mp4decrypt.exe");

        /// <summary>
        /// Starts the entire process; this is where the magic happens.
        /// </summary>
        private static void StartProcessor()
        {
            //executable validation
            if (!CheckRequiredExecutables())
                System.Console.WriteLine(
                    @"Process failed; required executable ffmpeg.exe and/or mp4decrypt.exe was not present.");
            else
            {
                //validation
                if (string.IsNullOrWhiteSpace(ManifestURL) || string.IsNullOrWhiteSpace(DecryptionKey))
                    System.Console.WriteLine(@"Process failed; one or more arguments were incorrect");
                else
                {
                    //key validation information
                    const int correctKeyLength = 32;
                    var actualKeyLength = DecryptionKey.Length;

                    //actual key validation
                    if (actualKeyLength != correctKeyLength)
                        System.Console.WriteLine(
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
                            System.Console.WriteLine(@"Process failed; master playlist URL was invalid");
                        else
                        {
                            //report progress
                            System.Console.WriteLine(@"Downloading master playlist");

                            //download master
                            var masterPlaylist = ManifestParsers.DownloadManifest(ManifestURL);

                            //check conformity
                            if (ManifestParsers.ManifestValid(masterPlaylist))
                            {
                                //decrypted file names
                                const string decryptedAudioFileName = @"audioDecrypted.mp4";
                                const string decryptedVideoFileName = @"videoDecrypted.mp4";

                                //encrypted file names
                                const string encryptedAudioFileName = @"audioEncrypted.bin";
                                const string encryptedVideoFileName = @"videoEncrypted.bin";

                                //directories for temporary storage
                                const string outputDir = @".\output";
                                const string workingDir = @".\tmp";

                                //decrypted file paths
                                var decryptedAudio = $@"{workingDir}\{decryptedAudioFileName}";
                                var decryptedVideo = $@"{workingDir}\{decryptedVideoFileName}";

                                //encrypted file paths
                                var encryptedAudio = $@"{workingDir}\{encryptedAudioFileName}";
                                var encryptedVideo = $@"{workingDir}\{encryptedVideoFileName}";

                                //output file path
                                var outputFile = $@"{outputDir}\{OutFileName}";

                                //ensure the directories exist
                                if (!Directory.Exists(outputDir))
                                    Directory.CreateDirectory(outputDir);
                                if (!Directory.Exists(workingDir))
                                    Directory.CreateDirectory(workingDir);

                                //start measuring audio download time
                                Timers.StartTimer(Timers.AudioDownloadTimer);

                                //download audio where audioFile is the path of the saved data
                                var audioFile =
                                    AudioDownloader.DownloadBestAudioFromMaster(masterPlaylist, ManifestURL,
                                        encryptedAudio);

                                //stop measuring audio download time
                                Timers.StopTimer(Timers.AudioDownloadTimer);

                                //start measuring video download time
                                Timers.StartTimer(Timers.VideoDownloadTimer);

                                //download video where videoFile is the path of the saved data
                                var videoFile =
                                    VideoDownloader.DownloadBestVideoFromMaster(masterPlaylist, ManifestURL,
                                        encryptedVideo);

                                //stop measuring video download time
                                Timers.StopTimer(Timers.VideoDownloadTimer);

                                //report progress
                                System.Console.WriteLine(@"Attempting decryption on audio");

                                //start measuring audio decrypt time
                                Timers.StartTimer(Timers.AudioDecryptTimer);

                                //decrypt audio stream
                                External.DoDecrypt(audioFile, decryptedAudio, DecryptionKey);

                                //stop measuring audio decrypt time
                                Timers.StopTimer(Timers.AudioDecryptTimer);

                                //report progress
                                System.Console.WriteLine(@"Attempting decryption on video");

                                //start measuring video decrypt time
                                Timers.StartTimer(Timers.VideoDecryptTimer);

                                //decrypt video stream
                                External.DoDecrypt(videoFile, decryptedVideo, DecryptionKey);

                                //stop measuring video decrypt time
                                Timers.StopTimer(Timers.VideoDecryptTimer);

                                //report progress
                                System.Console.WriteLine("\nDecryption procedure completed");
                                System.Console.WriteLine("Attempting stream remux");

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
                                    System.Console.WriteLine(@"Remux process completed");
                                }
                                else
                                    System.Console.WriteLine(
                                        @"FFMPEG Mux failed because one or more decrypted files were not available; check your key and try again.");
                            }
                            else
                                System.Console.WriteLine(
                                    @"Process failed; master playlist does not conform and is therefore invalid.");
                        }
                    }
                }
            }
        }
    }
}