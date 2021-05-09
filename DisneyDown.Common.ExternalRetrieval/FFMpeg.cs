// ReSharper disable UnusedMember.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable InconsistentNaming

using DisneyDown.Common.Net;
using DisneyDown.Common.Security;
using DisneyDown.Common.Util.Kit;
using System;

namespace DisneyDown.Common.ExternalRetrieval
{
    public static class FFMpeg
    {
        public static bool DownloadAndProcess()
        {
            try
            {
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
                if (!string.IsNullOrWhiteSpace(Endpoints.FFMpegDownloadUrl))
                {
                    //report download
                    ConsoleWriters.ConsoleWriteInfo(@"Downloading FFMPEG...");

                    //perform download
                    var ffmpeg = ResourceGrab.GrabBytes(Endpoints.FFMpegDownloadUrl);

                    //validation
                    if (ffmpeg?.Length > 0)
                    {
                        //report checksum calculation
                        ConsoleWriters.ConsoleWriteInfo(@"Calculating checksum...");

                        //checksum calculation
                        var checksum = Sha256Helper.Sha256ToHex(Sha256Helper.CalculateSha256Hash(ffmpeg));

                        //report checksum retrieval
                        ConsoleWriters.ConsoleWriteInfo(@"Retrieving valid checksum...");

                        //valid checksum retrieval
                        var validChecksum = ResourceGrab.GrabString(Endpoints.FFMpegChecksumUrl);

                        //report comparison of checksums
                        ConsoleWriters.ConsoleWriteInfo(@"Comparing checksums...");

                        //checksum comparison
                        var valid = string.Equals(checksum, validChecksum, StringComparison.CurrentCultureIgnoreCase); ;

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