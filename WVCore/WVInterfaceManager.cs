using DisneyDown.Common.Net;
using DisneyDown.Common.Util.Kit;
using System.Text;
using WVCore.Schemas;

// ReSharper disable InconsistentNaming

namespace WVCore
{
    public static class WVInterfaceManager
    {
        private static string DisneyServiceCertificateUrl => "https://playback-certs.bamgrid.com/static/v1.0/widevine.bin";

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

        public static string GetCdmHexKeyForPssh(string pssh, WVInterfaceConfig config)
        {
            try
            {
                ConsoleWriters.ConsoleWriteInfo(@"Checking CDM files");
                if (WVDeviceManager.DeviceFilesPresent())
                {
                    var clientBlob = new FileInfo(WVDeviceManager.DeviceIdBlobFile);
                    var clientKey = new FileInfo(WVDeviceManager.DeviceKeyFile);
                    var cdm = new WVApi(clientBlob, clientKey);
                    ConsoleWriters.ConsoleWriteInfo(@"Requesting Widevine challenge");
                    var chl = cdm.GetChallenge(pssh, config.LicenceCertificate);
                    if (chl.Length > 0)
                    {
                        ConsoleWriters.ConsoleWriteInfo(@"Challenge generated; forwarding it to the server");
                        var hdr = new Dictionary<string, string>
                            {
                                { @"Authorization", $"Bearer {EncodeNonAsciiCharacters(config.LicenceAuthorization)}" },
                                { @"User-Agent", config.LicenceClient }
                            };

                        foreach (var (key, value) in
                                 config.LicenceHeaders.Where(h => h.Key.ToLower() != @"authorization" && h.Key.ToLower() != "user-agent"))
                        {
                            hdr.Add(key, value);
                        }

                        var lic = WVHttpUtil.PostData(config.LicenceServer, hdr, chl);
                        if (lic.Length > 0)
                        {
                            if (lic.IsValidJsonBytes())
                            {
                                var e = WVErrorListing.FromJson(Encoding.Default.GetString(lic));
                                ConsoleWriters.ConsoleWriteError(@"License server error(s):");
                                foreach (var m in e.Errors)
                                {
                                    ConsoleWriters.ConsoleWriteError($"- '{m.Code}'");
                                }

                                return null!;
                            }

                            ConsoleWriters.ConsoleWriteInfo(
                                @"Received a license response; decoding within CDM");
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