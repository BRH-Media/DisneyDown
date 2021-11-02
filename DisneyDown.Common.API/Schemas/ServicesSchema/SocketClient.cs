using System.Collections.Generic;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class SocketClient
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Dictionary<string, SubmitMercadoPayment> Endpoints { get; set; }

        [JsonProperty("extras")]
        public StickyExtras Extras { get; set; }
    }
}