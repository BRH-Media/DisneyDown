using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class TokenClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Endpoints7 Endpoints { get; set; }

        [JsonProperty("extras")]
        public IndigoExtras Extras { get; set; }
    }
}