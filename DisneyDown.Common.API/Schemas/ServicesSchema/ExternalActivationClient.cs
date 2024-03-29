﻿using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class ExternalActivationClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public CunningEndpoints Endpoints { get; set; }
    }
}