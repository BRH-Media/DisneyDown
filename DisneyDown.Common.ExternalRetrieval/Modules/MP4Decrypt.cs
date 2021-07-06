﻿using DisneyDown.Common.Net;
using DisneyDown.Common.Security;
using DisneyDown.Common.Util.Kit;
using System;

// ReSharper disable UnusedMember.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedVariable

namespace DisneyDown.Common.ExternalRetrieval.Modules
{
    public static class MP4Decrypt
    {
        public static bool DownloadAndProcess()
        {
            try
            {
                //first step is XML endpoints extraction
                Endpoints.EnsureXml();

                //download archive
                var archive = FetchArchive();

                //validation
                if (archive?.Length > 0)
                {
                    //report extraction
                    ConsoleWriters.ConsoleWriteInfo(@"Extracting MP4Decrypt and MP4Dump...");

                    //required constants
                    const string mp4DecryptFileName = @"mp4decrypt.exe";
                    const string mp4DumpFileName = @"mp4dump.exe";

                    //execute extraction
                    if (ArchiveHandler.ExtractSingleFile(archive, mp4DecryptFileName) && ArchiveHandler.ExtractSingleFile(archive, mp4DumpFileName))
                    {
                        //report success
                        ConsoleWriters.ConsoleWriteSuccess(@"MP4Decrypt and MP4Dump successfully extracted");

                        return true;
                    }

                    //report failure
                    ConsoleWriters.ConsoleWriteError(@"MP4Decrypt and MP4Dump extraction failed");
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"MP4Decrypt and MP4Dump download and extract error: {ex.Message}");
            }

            //default
            return false;
        }

        public static byte[] FetchArchive()
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(Globals.SystemEndpoints.MP4DecryptDownloadUrl))
                {
                    //report download
                    ConsoleWriters.ConsoleWriteInfo(@"Downloading MP4Decrypt and MP4Dump...");

                    //perform download
                    var mp4decrypt = ResourceGrab.GrabBytes(Globals.SystemEndpoints.MP4DecryptDownloadUrl);

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
                        var validChecksum = Globals.SystemEndpoints.MP4DecryptChecksum;

                        //report comparison of checksums
                        ConsoleWriters.ConsoleWriteInfo(@"Comparing checksums...");

                        //checksum comparison
                        var valid = string.Equals(checksum, validChecksum, StringComparison.CurrentCultureIgnoreCase);

                        //report result
                        if (valid)

                            //successful download
                            ConsoleWriters.ConsoleWriteSuccess(@"MP4Decrypt and MP4Dump successfully downloaded");
                        else

                            //download error
                            ConsoleWriters.ConsoleWriteError(@"MP4Decrypt and MP4Dump download error: checksums do not match");

                        //result
                        return valid ? mp4decrypt : null;
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"MP4Decrypt and MP4Dump download error: {ex}");
            }

            //default
            return null;
        }
    }
}