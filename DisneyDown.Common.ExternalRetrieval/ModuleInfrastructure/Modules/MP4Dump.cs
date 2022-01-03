using DisneyDown.Common.Net;
using DisneyDown.Common.Util.Kit;
using System;

namespace DisneyDown.Common.ExternalRetrieval.ModuleInfrastructure.Modules
{
    public class MP4DumpModuleHandler : IExternalModule
    {
        public bool DownloadAndProcess()
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
                    ConsoleWriters.ConsoleWriteInfo(@"Extracting MP4Dump...");

                    //execute extraction
                    if (ArchiveHandler.ExtractAllFiles(archive, Globals.SystemEndpoints.MP4DumpEndpoint.FileNames))
                    {
                        //report success
                        ConsoleWriters.ConsoleWriteSuccess(@"MP4Dump successfully extracted");

                        return true;
                    }

                    //report failure
                    ConsoleWriters.ConsoleWriteError(@"MP4Dump extraction failed");
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"MP4Dump download and extract error: {ex.Message}");
            }

            //default
            return false;
        }

        public byte[] FetchArchive()
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(Globals.SystemEndpoints.MP4DumpEndpoint.DownloadUrl))
                {
                    //report download
                    ConsoleWriters.ConsoleWriteInfo(@"Downloading MP4Dump...");

                    //perform download
                    var mp4dump = ResourceGrab.GrabBytes(Globals.SystemEndpoints.MP4DumpEndpoint.DownloadUrl);

                    //validation
                    if (mp4dump?.Length > 0)
                    {
                        //report checksum calculation
                        ConsoleWriters.ConsoleWriteInfo(@"Calculating checksum...");

                        //checksum calculation
                        var checksum = Globals.SystemEndpoints.MP4DumpEndpoint.Checksum.CalculateChecksumFromAlgorithm(mp4dump);

                        //report checksum retrieval
                        ConsoleWriters.ConsoleWriteInfo(@"Retrieving valid checksum...");

                        //valid checksum retrieval
                        var validChecksum = Globals.SystemEndpoints.MP4DumpEndpoint.Checksum.RetrieveChecksum();

                        //report
                        ConsoleWriters.ConsoleWriteInfo($"Official checksum: {validChecksum}");

                        //report comparison of checksums
                        ConsoleWriters.ConsoleWriteInfo(@"Comparing checksums...");

                        //checksum comparison
                        var valid = string.Equals(checksum, validChecksum, StringComparison.CurrentCultureIgnoreCase);

                        //report result
                        if (valid)

                            //successful download
                            ConsoleWriters.ConsoleWriteSuccess(@"MP4Dump successfully downloaded");
                        else

                            //download error
                            ConsoleWriters.ConsoleWriteError(@"MP4Dump download error: checksums do not match");

                        //result
                        return valid ? mp4dump : null;
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"MP4Dump download error: {ex}");
            }

            //default
            return null;
        }
    }
}