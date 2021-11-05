using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class Url
    {
        [JsonProperty("url")]
        public Uri UrlUrl { get; set; }
    }
}