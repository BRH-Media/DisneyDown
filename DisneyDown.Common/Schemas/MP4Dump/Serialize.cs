using Newtonsoft.Json;

namespace DisneyDown.Common.Schemas.MP4Dump
{
    public static class Serialize
    {
        public static string ToJson(this Mp4DumpTopLevelAtom[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}