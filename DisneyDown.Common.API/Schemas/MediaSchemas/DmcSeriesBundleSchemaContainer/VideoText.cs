using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class VideoText
    {
        [JsonProperty("description")]
        public PurpleDescription Description { get; set; }

        [JsonProperty("title")]
        public PurpleTitle Title { get; set; }
    }
}