﻿using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class VideoArt
    {
        [JsonProperty("mediaMetadata")]
        public VideoArtMediaMetadata MediaMetadata { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; }
    }
}