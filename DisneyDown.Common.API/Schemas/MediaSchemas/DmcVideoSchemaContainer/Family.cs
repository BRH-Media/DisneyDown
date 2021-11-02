using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class Family
    {
        [JsonProperty("encodedFamilyId")]
        public string EncodedFamilyId { get; set; }

        [JsonProperty("familyId")]
        public string FamilyId { get; set; }

        [JsonProperty("parent")]
        public bool Parent { get; set; }

        [JsonProperty("parentRef")]
        public ParentRef ParentRef { get; set; }

        [JsonProperty("sequenceNumber")]
        public object SequenceNumber { get; set; }
    }
}