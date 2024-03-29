﻿using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class PurchaseClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Endpoints2 Endpoints { get; set; }
    }
}