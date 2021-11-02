using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class BamIdentityExtras
    {
        [JsonProperty("expirationBufferSeconds")]
        public long ExpirationBufferSeconds { get; set; }
    }
}