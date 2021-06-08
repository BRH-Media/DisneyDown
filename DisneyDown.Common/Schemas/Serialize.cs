using Newtonsoft.Json;

namespace DisneyDown.Common.Schemas
{
    public static class Serialize
    {
        public static string ToJson(this Mp4DumpSchema[] self) => JsonConvert.SerializeObject(self, DisneyDown.Common.Schemas.Converter.Settings);
    }
}