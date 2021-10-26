// ReSharper disable UnusedMember.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable InconsistentNaming

using DisneyDown.Common.Net;
using DisneyDown.Common.Util.Kit;
using System;

namespace DisneyDown.Common.ExternalRetrieval.Modules
{
    public static class FFMpeg
    {
        public static bool DownloadAndProcess()
        {
            try
            {
                //first step is XML endpoints extraction
                Endpoints.EnsureJson();

                //download archive
                var archive = FetchArchive();

                //validation
                if (archive?.Length > 0)
                {
                    //report extraction
                    ConsoleWriters.ConsoleWriteInfo(@"Extracting FFMPEG...");

                    //required constants
                    const string fileName = @"ffmpeg.exe";

                    //execute extraction
                    if (ArchiveHandler.ExtractSingleFile(archive, fileName))
                    {
                        //report success
                        ConsoleWriters.ConsoleWriteSuccess(@"FFMPEG successfully extracted");

                        //successful
                        return true;
                    }

                    //report failure
                    ConsoleWriters.ConsoleWriteError(@"FFMPEG extraction failed");
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"FFMPEG download and extract error: {ex.Message}");
            }

            //default
            return false;
        }

        public static byte[] FetchArchive()
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(Globals.SystemEndpoints.FFMpegEndpoint.DownloadUrl))
                {
                    //report download
                    ConsoleWriters.ConsoleWriteInfo(@"Downloading FFMPEG...");

                    //perform download
                    var ffmpeg = ResourceGrab.GrabBytes(Globals.SystemEndpoints.FFMpegEndpoint.DownloadUrl);

                    //validation
                    if (ffmpeg?.Length > 0)
                    {
                        //report checksum calculation
                        ConsoleWriters.ConsoleWriteInfo(@"Calculating checksum...");

                        //checksum calculation
                        var checksum = Globals.SystemEndpoints.FFMpegEndpoint.Checksum.CalculateChecksumFromAlgorithm(ffmpeg);

                        //report checksum retrieval
                        ConsoleWriters.ConsoleWriteInfo(@"Retrieving valid checksum...");

                        //valid checksum retrieval
                        var validChecksum = Globals.SystemEndpoints.FFMpegEndpoint.Checksum.RetrieveChecksum();

                        //report
                        ConsoleWriters.ConsoleWriteInfo($"Official checksum: {validChecksum}");

                        //report comparison of checksums
                        ConsoleWriters.ConsoleWriteInfo(@"Comparing checksums...");

                        //checksum comparison
                        var valid = string.Equals(checksum, validChecksum, StringComparison.CurrentCultureIgnoreCase);

                        //report result
                        if (valid)

                            //successful download
                            ConsoleWriters.ConsoleWriteSuccess(@"FFMPEG successfully downloaded");
                        else

                            //download error
                            ConsoleWriters.ConsoleWriteError(@"FFMPEG download error: checksums do not match");

                        //result
                        return valid ? ffmpeg : null;
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"FFMPEG download error: {ex}");
            }

            //default
            return null;
        }
    }
}