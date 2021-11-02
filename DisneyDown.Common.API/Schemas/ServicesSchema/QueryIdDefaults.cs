using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class QueryIdDefaults
    {
        [JsonProperty("addToWatchlist")]
        public string AddToWatchlist { get; set; }

        [JsonProperty("clearWatchlist")]
        public string ClearWatchlist { get; set; }

        [JsonProperty("removeFromWatchlist")]
        public string RemoveFromWatchlist { get; set; }
    }
}