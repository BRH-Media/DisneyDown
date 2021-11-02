using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class CommerceClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public FluffyEndpoints Endpoints { get; set; }

        [JsonProperty("extras")]
        public PurpleExtras Extras { get; set; }
    }
}