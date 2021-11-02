using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class DmcSeriesBundleSchema
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        public static DmcSeriesBundleSchema FromJson(string json) => JsonConvert.DeserializeObject<DmcSeriesBundleSchema>(json, ApiJsonConverter.Settings);
    }
}