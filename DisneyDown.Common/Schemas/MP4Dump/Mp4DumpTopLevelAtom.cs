﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using DisneyDown.Common.Schemas.;
//
//    var mp4DumpSchema = Mp4DumpSchema.FromJson(jsonString);

using System;
using System.Collections.Generic;
using DisneyDown.Common.Schemas.MP4Dump;
using Newtonsoft.Json;
using Converter = DisneyDown.Common.Util.Converter;

namespace DisneyDown.Common.Schemas
{
public partial class Mp4DumpTopLevelAtom
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("header_size")]
        public long HeaderSize { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("major_brand", NullValueHandling = NullValueHandling.Ignore)]
        public string MajorBrand { get; set; }

        [JsonProperty("minor_version", NullValueHandling = NullValueHandling.Ignore)]
        public long? MinorVersion { get; set; }

        [JsonProperty("compatible_brand", NullValueHandling = NullValueHandling.Ignore)]
        public string CompatibleBrand { get; set; }

        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public Mp4DumpSchemaChild[] Children { get; set; }
    }

public partial class Mp4DumpTopLevelAtom
    {
        public static Mp4DumpTopLevelAtom[] FromJson(string json) => JsonConvert.DeserializeObject<Mp4DumpTopLevelAtom[]>(json, Converter.Settings);
    }
}
