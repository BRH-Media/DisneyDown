// ReSharper disable UnusedMember.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable InconsistentNaming

using DisneyDown.Common.Net;
using DisneyDown.Common.Security;
using DisneyDown.Common.Util.Kit;
using System;

namespace DisneyDown.Common.ExternalRetrieval
{
    public static class MP4Decrypt
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
                    ConsoleWriters.ConsoleWriteInfo(@"Extracting MP4Decrypt...");

                    //required constants
                    const string fileName = @"mp4decrypt.exe";

                    //execute extraction
                    if (ArchiveHandler.ExtractSingleFile(archive, fileName))
                    {
                        //report success
                        ConsoleWriters.ConsoleWriteSuccess(@"MP4Decrypt successfully extracted");

                        //success
                        return true;
                    }

                    //report failure
                    ConsoleWriters.ConsoleWriteError(@"MP4Decrypt extraction failed");
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"MP4Decrypt download and extract error: {ex.Message}");
            }

            //default
            return false;
        }

        public static byte[] FetchArchive()
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(Endpoints.MP4DecryptDownloadUrl))
                {
                    //report download
                    ConsoleWriters.ConsoleWriteInfo(@"Downloading MP4Decrypt...");

                    //perform download
                    var mp4decrypt = ResourceGrab.GrabBytes(Endpoints.MP4DecryptDownloadUrl);

                    //validation
                    if (mp4decrypt?.Length > 0)
                    {
                        //report checksum calculation
                        ConsoleWriters.ConsoleWriteInfo(@"Calculating checksum...");

                        //checksum calculation
                        var checksum = Sha1Helper.Sha1ToHex(Sha1Helper.CalculateSha1Hash(mp4decrypt));

                        //report checksum retrieval
                        ConsoleWriters.ConsoleWriteInfo(@"Retrieving valid checksum...");

                        //valid checksum retrieval
                        var validChecksum = Endpoints.MP4DecryptChecksum;

                        //report comparison of checksums
                        ConsoleWriters.ConsoleWriteInfo(@"Comparing checksums...");

                        //checksum comparison
                        var valid = string.Equals(checksum, validChecksum, StringComparison.CurrentCultureIgnoreCase); ;

                        //report result
                        if (valid)

                            //successful download
                            ConsoleWriters.ConsoleWriteSuccess(@"MP4Decrypt successfully downloaded");
                        else

                            //download error
                            ConsoleWriters.ConsoleWriteError(@"MP4Decrypt download error: checksums do not match");

                        //result
                        return valid ? mp4decrypt : null;
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"MP4Decrypt download error: {ex}");
            }

            //default
            return null;
        }
    }
}