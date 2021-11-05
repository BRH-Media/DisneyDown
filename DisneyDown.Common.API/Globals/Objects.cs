using DisneyDown.Common.API.Schemas.ServicesSchema;
using DisneyDown.Common.API.Structures;

namespace DisneyDown.Common.API.Globals
{
    public static class Objects
    {
        public static ServiceInformation Services { get; set; } = new ServiceInformation();
        public static ApiConfiguration Configuration { get; set; } = ApiConfiguration.GetApiConfiguration();
    }
}