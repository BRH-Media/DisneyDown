﻿using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class TelemetryClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Endpoints6 Endpoints { get; set; }
    }
}