using Newtonsoft.Json;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class DmcVideoBundleSchema
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        public static DmcVideoBundleSchema FromJson(string json) => JsonConvert.DeserializeObject<DmcVideoBundleSchema>(json, ApiJsonConverter.Settings);
    }
}