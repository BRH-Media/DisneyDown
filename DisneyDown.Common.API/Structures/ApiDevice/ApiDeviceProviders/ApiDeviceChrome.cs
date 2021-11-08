// ReSharper disable StringLiteralTypo

namespace DisneyDown.Common.API.Structures.ApiDevice.ApiDeviceProviders
{
    public class ApiDeviceChrome : ApiDevice
    {
        public ApiDeviceChrome()
        {
            //setup device
            BamSdkPlatform = "linux";
            BamSdkVersion = "4.11";
            ApiKey = "ZGlzbmV5JmJyb3dzZXImMS4wLjA.Cu56AgSfBTDag5NiRA81oLHkDZfu5L3CKadnefEAY84";
            Runtime = "chrome";
            DeviceFamily = "browser";
            DeviceProfile = "linux";
            Attributes = new ApiDeviceAttributes
            {
                Manufacturer = @"Google",
                Model = "Chrome",
                OperatingSystem = "Linux",
                OperatingSystemVersion = "4.11"
            };
            Attributes.OsDeviceIds.Add(new ApiDeviceId());
        }
    }
}