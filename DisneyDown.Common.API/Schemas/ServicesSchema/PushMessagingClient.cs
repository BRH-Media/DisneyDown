﻿using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class PushMessagingClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Endpoints3 Endpoints { get; set; }
    }
}