using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisneyDown.Common.Net;

namespace DisneyDown.Common.KeySystem.Widevine
{
    public static class WidevineInterfaceManager
    {
        private static string DisneyServiceCertificateUrl => "https://playback-certs.bamgrid.com/static/v1.0/widevine.bin";

        public static string GetCdmHexKeyForPssh(string pssh)
        {
            try
            {
                var cdm = new 
            }
            catch (Exception ex)
            {
                //nothing
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
