using DisneyDown.Common.API.Schemas;
using DisneyDown.Common.API.Structures.ApiDevice;

namespace DisneyDown.Common.API.Globals
{
    public static class Objects
    {
        public static ServiceInformation Services { get; set; } = new ServiceInformation();
        public static ApiDevice CurrentDevice { get; set; } = new ApiDeviceChrome();
    }
}