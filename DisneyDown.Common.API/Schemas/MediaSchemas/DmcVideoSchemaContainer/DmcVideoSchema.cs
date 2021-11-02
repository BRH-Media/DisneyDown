using Newtonsoft.Json;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class DmcVideoSchema
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        public static DmcVideoSchema FromJson(string json) => JsonConvert.DeserializeObject<DmcVideoSchema>(json, ApiJsonConverter.Settings);
    }
}