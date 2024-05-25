using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace WVCore.Schemas
{
    public static class WVGenericSchemaConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}