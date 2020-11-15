﻿using DisneyDown.Common.Processors;
using DisneyDown.Common.Processors.Downloaders;
using DisneyDown.Common.Processors.Parsers;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

// ReSharper disable InconsistentNaming

namespace DisneyDown.Console
{
    public static class Program
    {
        public static string ExecutableName => Path.GetFileName(Assembly.GetEntryAssembly()?.Location);
        public static string DecryptionKey { get; set; } = @"";
        public static string ManifestURL { get; set; } = @"";
        public static string OutFileName { get; set; } = @"decryptedDisney.mkv";

        private static void PrintUsage()
        {
            System.Console.WriteLine($@"usage: {ExecutableName} widevine_hex_key master_manifest_URL [output_file_name]");
            System.Console.WriteLine(@" options:");
            System.Console.WriteLine(@"  widevine_hex_key    - 32 character content decryption key");
            System.Console.WriteLine(@"  master_manifest_URL - m3u8 master manifest URL; do not input a locally available manifest.");
            System.Console.WriteLine(@"  output_file_name    - Name of the remuxed file in the .\output folder");
        }

        private static void Main(string[] args)
        {
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
        }

        private static bool CheckRequiredExecutables() => File.Exists(@"ffmpeg.exe") && File.Exists(@"mp4decrypt.exe");

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

                                //download audio and video data
                                var audioFile =
                                    AudioDownloader.DownloadBestAudioFromMaster(masterPlaylist, ManifestURL,
                                        encryptedAudio);
                                var videoFile =
                                    VideoDownloader.DownloadBestVideoFromMaster(masterPlaylist, ManifestURL,
                                        encryptedVideo);

                                //report progress
                                System.Console.WriteLine(@"Attempting decryption on audio");

                                //decrypt audio stream
                                External.DoDecrypt(audioFile, decryptedAudio, DecryptionKey);

                                //report progress
                                System.Console.WriteLine(@"Attempting decryption on video");

                                //decrypt video stream
                                External.DoDecrypt(videoFile, decryptedVideo, DecryptionKey);

                                //report progress
                                System.Console.WriteLine("\nDecryption procedure completed");
                                System.Console.WriteLine("Attempting stream remux");

                                //attempt mux audio and video together (only if the decryption succeeded)
                                if (File.Exists(decryptedVideo) && File.Exists(decryptedAudio))
                                {
                                    External.DoMux(decryptedAudio, decryptedVideo, outputFile);
                                    System.Console.WriteLine(@"Remux process completed");
                                    System.Console.WriteLine("\nDone!");
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