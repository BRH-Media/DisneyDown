using System.Text;
using DisneyDown.Common.Net;
using DisneyDown.Common.Util.Kit;

namespace WVCore
{
    public static class WVInterfaceManager
    {
        private static string DisneyServiceCertificateUrl => "https://playback-certs.bamgrid.com/static/v1.0/widevine.bin";
        private static string DisneyLicenseServerUrl => @"https://disney.playback.edge.bamgrid.com/widevine/v1/obtain-license";

        private static string EncodeNonAsciiCharacters(string value)
        {
            var sb = new StringBuilder();
            foreach (var c in value)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII  
                    var encodedValue = "\\u" + ((int)c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string GetCdmHexKeyForPssh(string pssh, string authToken = "")
        {
            try
            {
                ConsoleWriters.ConsoleWriteInfo(@"Checking CDM files");
                if (WVDeviceManager.DeviceFilesPresent())
                {
                    ConsoleWriters.ConsoleWriteInfo(@"Requesting service certificate");
                    var certBytes = GetDisneyWidevineCertificateB64();
                    if (!string.IsNullOrWhiteSpace(certBytes))
                    {
                        ConsoleWriters.ConsoleWriteSuccess(@"Successfully obtained certificate data");
                        var clientBlob = new FileInfo(WVDeviceManager.DeviceIdBlobFile);
                        var clientKey = new FileInfo(WVDeviceManager.DeviceKeyFile);
                        var cdm = new WVApi(clientBlob, clientKey);
                        ConsoleWriters.ConsoleWriteInfo(@"Requesting Widevine challenge");
                        var chl = cdm.GetChallenge(pssh, certBytes);
                        if (chl.Length > 0)
                        {
                            ConsoleWriters.ConsoleWriteInfo(@"Challenge generated; forwarding it to the server");
                            var hdr = new Dictionary<string, string>
                            {
                                { @"Authorization", $"Bearer {EncodeNonAsciiCharacters(authToken)}" }
                            };
                            var lic = HttpUtil.PostData(DisneyLicenseServerUrl, hdr, chl);
                            if (lic.Length > 0)
                            {
                                ConsoleWriters.ConsoleWriteInfo(@"Received a license response; decoding within CDM");
                                var b64 = Convert.ToBase64String(lic);
                                if (cdm.ProvideLicense(b64))
                                {
                                    var key = cdm.GetKeys();
                                    if (key.Count > 0)
                                    {
                                        return key[0].ToString();
                                    }
                                }
                            }
                            else
                            {
                                ConsoleWriters.ConsoleWriteError(@"Invalid response from license server");
                            }
                        }
                        else
                        {
                            ConsoleWriters.ConsoleWriteError(@"Invalid Widevine challenge");
                        }
                    }
                    else
                    {
                        ConsoleWriters.ConsoleWriteError(@"Invalid service certificate");
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleWriters.ConsoleWriteError($"Critical error: {ex}");
            }

            //default
            return null;
        }

        public static byte[] GetDisneyWidevineCertificate()
        {
            try
            {
                //get data
                var data = ResourceGrab.GrabBytes(DisneyServiceCertificateUrl);

                //proceed
                return data;
            }
            catch (Exception ex)
            {
                //nothing
            }

            //default
            return null;
        }

        public static string GetDisneyWidevineCertificateB64()
        {
            try
            {
                //get data
                var data = GetDisneyWidevineCertificate();

                if (data?.Length > 0)
                {
                    return Convert.ToBase64String(data);
                }
            }
            catch (Exception ex)
            {
                //nothing
            }

            //default
            return null;
        }
    }
}