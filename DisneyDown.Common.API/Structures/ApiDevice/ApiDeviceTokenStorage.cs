using DisneyDown.Common.API.Enums;

namespace DisneyDown.Common.API.Structures.ApiDevice
{
    public class ApiDeviceTokenStorage
    {
        public ApiDeviceTokenObject Device { get; } = new ApiDeviceTokenObject(ExchangeType.DEVICE);

        public ApiDeviceTokenObject Account { get; } = new ApiDeviceTokenObject(ExchangeType.ACCOUNT);

        public ApiDeviceTokenObject Identity { get; } = new ApiDeviceTokenObject(ExchangeType.IDENTITY);
        public ApiDeviceTokenObject Refresh { get; } = new ApiDeviceTokenObject(ExchangeType.REFRESH);
    }
}