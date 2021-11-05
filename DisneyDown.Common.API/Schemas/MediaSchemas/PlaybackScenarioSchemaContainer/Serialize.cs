using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public static class Serialize
    {
        public static string ToJson(this PlaybackScenarioSchema self) => JsonConvert.SerializeObject(self, ApiJsonConverter.Settings);
    }
}