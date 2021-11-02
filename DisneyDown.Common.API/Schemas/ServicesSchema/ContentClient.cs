using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class ContentClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public TentacledEndpoints Endpoints { get; set; }

        [JsonProperty("extras")]
        public FluffyExtras Extras { get; set; }
    }
}