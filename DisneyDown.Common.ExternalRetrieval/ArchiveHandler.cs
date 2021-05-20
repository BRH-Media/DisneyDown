using DisneyDown.Common.Util.Kit;
using System;
using System.IO;
using System.IO.Compression;

// ReSharper disable InvertIf

namespace DisneyDown.Common.ExternalRetrieval
{
    public static class ArchiveHandler
    {
        public static bool ExtractSingleFile(byte[] archiveData, string fileName, string extractDir = @"")
        {
            try
            {
                //validate path
                if (string.IsNullOrWhiteSpace(extractDir))

                    //set default value
                    extractDir = Globals.AssemblyDirectory;

                //null validation
                if (archiveData?.Length > 0 && !string.IsNullOrWhiteSpace(fileName) && Directory.Exists(extractDir))
                {
                    //extract location
                    var extractFileLocation = $@"{extractDir}\{fileName}";

                    //load stream from archive raw data
                    using (var zippedStream = new MemoryStream(archiveData))
                    {
                        //create new archive handler
                        using (var archive = new ZipArchive(zippedStream))
                        {
                            //go through each entry
                            foreach (var e in archive.Entries)
                            {
                                //check name
                                if (string.Equals(e.Name, fileName, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    //read into a new stream
                                    using (var unzippedEntryStream = e.Open())
                                    {
                                        //new byte-stream
                                        using (var ms = new MemoryStream())
                                        {
                                            //copy to stream
                                            unzippedEntryStream.CopyTo(ms);

                                            //close unzipped stream
                                            unzippedEntryStream.Close();

                                            //copy to bytes
                                            var unzippedArray = ms.ToArray();

                                            //close memory stream
                                            ms.Close();

                                            //output to file
                                            File.WriteAllBytes(extractFileLocation, unzippedArray);

                                            //exit loop
                                            return File.Exists(extractFileLocation);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Archive extraction error: {ex.Message}");
            }

            //default
            return false;
        }
    }
}