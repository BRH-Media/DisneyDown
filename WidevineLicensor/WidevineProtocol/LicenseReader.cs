using System;
using VideoWidevineServer.Sdk;

namespace WidevineLicensor.WidevineProtocol
{
    public static class LicenseReader
    {
        public static void ConvertResponse(string base64Response)
        {
            try
            {
                //convert from base64
                var decoded = base64Response.FromBase64();

                //read in signed message
                var licenseResponse = new SignedMessage();
                //licenseResponse.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}