using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class SessionClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Endpoints4 Endpoints { get; set; }
    }
}