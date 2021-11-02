using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class DrmClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public IndecentEndpoints Endpoints { get; set; }

        [JsonProperty("extras")]
        public TentacledExtras Extras { get; set; }
    }
}