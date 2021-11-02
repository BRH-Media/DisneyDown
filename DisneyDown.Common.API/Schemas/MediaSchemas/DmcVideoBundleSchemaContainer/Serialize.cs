using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public static class Serialize
    {
        public static string ToJson(this DmcVideoBundleSchema self) => JsonConvert.SerializeObject(self, DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer.Converter.Settings);
    }
}