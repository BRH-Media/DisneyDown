using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class Url
    {
        [JsonProperty("url")]
        public Uri UrlUrl { get; set; }
    }
}