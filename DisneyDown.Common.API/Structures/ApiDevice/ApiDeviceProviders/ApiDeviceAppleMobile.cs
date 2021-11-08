// ReSharper disable StringLiteralTypo

namespace DisneyDown.Common.API.Structures.ApiDevice.ApiDeviceProviders
{
    public class ApiDeviceAppleMobile : ApiDevice
    {
        public ApiDeviceAppleMobile()
        {
            //setup device
            BamSdkPlatform = "iPhone10,2";
            BamSdkVersion = "9.10.0";
            ApiKey = "ZGlzbmV5JmFwcGxlJjEuMC4w.H9L7eJvc2oPYwDgmkoar6HzhBJRuUUzt_PcaC3utBI4";
            Runtime = "ios";
            DeviceFamily = "apple";
            DeviceProfile = "iphone";
            Attributes = new ApiDeviceAttributes
            {
                Manufacturer = @"apple",
                Model = "iPhone10,2",
                OperatingSystem = "iOS",
                OperatingSystemVersion = "14.0"
            };
            Attributes.OsDeviceIds.Add(new ApiDeviceId
            {
                Type = @"apple.vendor.id"
            });
        }
    }
}