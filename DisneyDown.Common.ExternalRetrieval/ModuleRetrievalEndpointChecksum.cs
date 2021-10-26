using DisneyDown.Common.Net;
using DisneyDown.Common.Security.Hashing;
using DisneyDown.Common.Util.Kit;
using System;
using System.Text;

namespace DisneyDown.Common.ExternalRetrieval
{
    public class ModuleRetrievalEndpointChecksum
    {
        public string Content { get; set; } = @"";
        public string Algorithm { get; set; } = @"";

        public string CalculateChecksumFromAlgorithm(byte[] checkBytes)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(Algorithm))
                {
                    switch (Algorithm)
                    {
                        case @"sha1":
                            return Sha1Helper.CalculateSha1Hash(checkBytes).ToHex();

                        case @"sha256":
                            return Sha256Helper.CalculateSha256Hash(checkBytes).ToHex();

                        case @"md5":
                            return Md5Helper.CalculateMd5Hash(checkBytes).ToHex();
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError(
                    $"Error whilst trying to calculate an external module checksum: {ex.Message}");
            }

            //default
            return @"";
        }

        public string RetrieveChecksum()
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(Content))
                {
                    //create a format split
                    var checksum = Content.Split(':');

                    //validation
                    if (checksum.Length == 2)
                    {
                        var action = checksum[0];
                        var content = checksum[1];

                        //validation (both sides)
                        if (!string.IsNullOrWhiteSpace(action) && !string.IsNullOrWhiteSpace(content))
                        {
                            //decode checksum content
                            var contentDecoded = Encoding.Default.GetString(Convert.FromBase64String(content));

                            //validation
                            if (!string.IsNullOrWhiteSpace(contentDecoded))
                            {
                                //decide on what to do
                                switch (action)
                                {
                                    case @"fetch":

                                        //download the checksum and return it
                                        return ResourceGrab.GrabString(contentDecoded);

                                    case @"return":

                                        //return the checksum as-is
                                        return contentDecoded;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Error whilst trying to decode/retrieve external module checksum: {ex.Message}");
            }

            //default
            return @"";
        }
    }
}