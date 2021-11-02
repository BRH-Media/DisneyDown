using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class AccountClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Dictionary<string, Endpoint> Endpoints { get; set; }
    }
}