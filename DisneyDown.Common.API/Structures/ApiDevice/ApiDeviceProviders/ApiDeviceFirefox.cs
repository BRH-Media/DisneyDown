using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyDown.Common.API.Structures.ApiDevice.ApiDeviceProviders
{
    public class ApiDeviceFirefox : ApiDevice
    {
        public ApiDeviceFirefox()
        {
            //setup device
            BamSdkPlatform = "macosx";
            BamSdkVersion = "10.12.6";
            ApiKey = "ZGlzbmV5JmJyb3dzZXImMS4wLjA.Cu56AgSfBTDag5NiRA81oLHkDZfu5L3CKadnefEAY84";
            Runtime = "firefox";
            DeviceFamily = "browser";
            DeviceProfile = "macosx";
            Attributes = new ApiDeviceAttributes
            {
                Manufacturer = @"Apple",
                Model = "Firefox",
                OperatingSystem = "Mac OS X",
                OperatingSystemVersion = "10.12.6"
            };
            Attributes.OsDeviceIds.Add(new ApiDeviceId());
        }
    }
}
