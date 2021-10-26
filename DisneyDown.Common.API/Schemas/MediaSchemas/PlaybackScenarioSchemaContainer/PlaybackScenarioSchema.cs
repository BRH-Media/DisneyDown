using Newtonsoft.Json;
using System;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class PlaybackScenarioSchema
    {
        [JsonProperty("stream")]
        public Stream Stream { get; set; }

        [JsonProperty("tracking")]
        public PlaybackScenarioSchemaTracking Tracking { get; set; }

        [JsonProperty("playhead")]
        public Playhead Playhead { get; set; }

        [JsonProperty("thumbnails")]
        public Thumbnails Thumbnails { get; set; }

        public static PlaybackScenarioSchema FromJson(string json) => JsonConvert.DeserializeObject<PlaybackScenarioSchema>(json, Converter.Settings);
    }

    public class Playhead
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Stream
    {
        [JsonProperty("renditions")]
        public Renditions Renditions { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("variants")]
        public Variant[] Variants { get; set; }

        [JsonProperty("complete")]
        public Complete[] Complete { get; set; }
    }

    public class Attributes
    {
        [JsonProperty("drms")]
        public string[] Drms { get; set; }

        [JsonProperty("encryptionType")]
        public string EncryptionType { get; set; }

        [JsonProperty("audioSegmentTypes")]
        public object[] AudioSegmentTypes { get; set; }

        [JsonProperty("videoSegmentTypes")]
        public object[] VideoSegmentTypes { get; set; }

        [JsonProperty("videoRanges")]
        public string[] VideoRanges { get; set; }

        [JsonProperty("security")]
        public Security Security { get; set; }

        [JsonProperty("experienceContext")]
        public string ExperienceContext { get; set; }
    }

    public class Security
    {
        [JsonProperty("drmEndpointKey")]
        public string DrmEndpointKey { get; set; }
    }

    public class Complete
    {
        [JsonProperty("priority")]
        public long Priority { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("tracking")]
        public CompleteTracking Tracking { get; set; }
    }

    public class CompleteTracking
    {
        [JsonProperty("conviva")]
        public PurpleConviva Conviva { get; set; }

        [JsonProperty("telemetry")]
        public PurpleTelemetry Telemetry { get; set; }

        [JsonProperty("adEngine")]
        public PurpleAdEngine AdEngine { get; set; }

        [JsonProperty("qos")]
        public PurpleQos Qos { get; set; }
    }

    public class PurpleAdEngine
    {
        [JsonProperty("cdn")]
        public string Cdn { get; set; }

        [JsonProperty("corigin")]
        public string Corigin { get; set; }
    }

    public class PurpleConviva
    {
        [JsonProperty("cdnName")]
        public string CdnName { get; set; }

        [JsonProperty("locationName")]
        public string LocationName { get; set; }

        [JsonProperty("cdnVendor")]
        public string CdnVendor { get; set; }
    }

    public class PurpleQos
    {
        [JsonProperty("cdnWithOrigin")]
        public string CdnWithOrigin { get; set; }

        [JsonProperty("cdnName")]
        public string CdnName { get; set; }

        [JsonProperty("cdnVendor")]
        public string CdnVendor { get; set; }
    }

    public class PurpleTelemetry
    {
        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("cdn")]
        public string Cdn { get; set; }

        [JsonProperty("cdnPolicyId")]
        public string CdnPolicyId { get; set; }
    }

    public class Renditions
    {
        [JsonProperty("audio")]
        public Audio[] Audio { get; set; }

        [JsonProperty("subtitles")]
        public Subtitle[] Subtitles { get; set; }
    }

    public class Audio
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public class Subtitle
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("forced")]
        public bool Forced { get; set; }
    }

    public class Variant
    {
        [JsonProperty("bandwidth")]
        public long Bandwidth { get; set; }

        [JsonProperty("resolution")]
        public string Resolution { get; set; }

        [JsonProperty("videoBytes")]
        public long VideoBytes { get; set; }

        [JsonProperty("maxAudioRenditionBytes")]
        public long MaxAudioRenditionBytes { get; set; }

        [JsonProperty("maxSubtitleRenditionBytes")]
        public long MaxSubtitleRenditionBytes { get; set; }

        [JsonProperty("audioChannels")]
        public long AudioChannels { get; set; }

        [JsonProperty("videoRange")]
        public string VideoRange { get; set; }

        [JsonProperty("videoCodec")]
        public string VideoCodec { get; set; }

        [JsonProperty("audioType")]
        public string AudioType { get; set; }

        [JsonProperty("audioCodec")]
        public string AudioCodec { get; set; }

        [JsonProperty("averageBandwidth")]
        public long AverageBandwidth { get; set; }

        [JsonProperty("frameRate")]
        public double FrameRate { get; set; }
    }

    public class Thumbnails
    {
        [JsonProperty("bif")]
        public Bif Bif { get; set; }

        [JsonProperty("spritesheet")]
        public Bif Spritesheet { get; set; }

        [JsonProperty("bif-main")]
        public Bif BifMain { get; set; }
    }

    public class Bif
    {
        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("headers")]
        public Headers Headers { get; set; }
    }

    public class Headers
    {
        [JsonProperty("x-playback-scenario-name")]
        public string XPlaybackScenarioName { get; set; }

        [JsonProperty("accept")]
        public string Accept { get; set; }

        [JsonProperty("x-playback-request-id")]
        public string XPlaybackRequestId { get; set; }
    }

    public class PlaybackScenarioSchemaTracking
    {
        [JsonProperty("conviva")]
        public FluffyConviva Conviva { get; set; }

        [JsonProperty("telemetry")]
        public FluffyTelemetry Telemetry { get; set; }

        [JsonProperty("adEngine")]
        public FluffyAdEngine AdEngine { get; set; }

        [JsonProperty("qos")]
        public FluffyQos Qos { get; set; }
    }

    public class FluffyAdEngine
    {
        [JsonProperty("fguid")]
        public Guid Fguid { get; set; }
    }

    public class FluffyConviva
    {
        [JsonProperty("pbs")]
        public string Pbs { get; set; }

        [JsonProperty("fguid")]
        public Guid Fguid { get; set; }

        [JsonProperty("isFilterable")]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool IsFilterable { get; set; }

        [JsonProperty("imageAspectRatio")]
        public string ImageAspectRatio { get; set; }

        [JsonProperty("capabilities")]
        public string Capabilities { get; set; }

        [JsonProperty("experiments")]
        public string Experiments { get; set; }

        [JsonProperty("pool")]
        public string Pool { get; set; }

        [JsonProperty("encryptionType")]
        public string EncryptionType { get; set; }

        [JsonProperty("med")]
        public string Med { get; set; }

        [JsonProperty("userid")]
        public Guid Userid { get; set; }

        [JsonProperty("deviceId")]
        public Guid DeviceId { get; set; }

        [JsonProperty("baseDevice")]
        public string BaseDevice { get; set; }

        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("prt")]
        public string Prt { get; set; }

        [JsonProperty("videoSegmentTypes")]
        public string VideoSegmentTypes { get; set; }

        [JsonProperty("videoRanges")]
        public string VideoRanges { get; set; }

        [JsonProperty("videoCodecs")]
        public string VideoCodecs { get; set; }

        [JsonProperty("plt")]
        public string Plt { get; set; }

        [JsonProperty("experienceContext")]
        public string ExperienceContext { get; set; }

        [JsonProperty("conid")]
        public Guid Conid { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("productType")]
        public string ProductType { get; set; }
    }

    public class FluffyQos
    {
        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("fguid")]
        public Guid Fguid { get; set; }

        [JsonProperty("isFilterable")]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool IsFilterable { get; set; }

        [JsonProperty("imageAspectRatio")]
        public string ImageAspectRatio { get; set; }

        [JsonProperty("capabilities")]
        public string Capabilities { get; set; }

        [JsonProperty("pool")]
        public string Pool { get; set; }

        [JsonProperty("mediaType")]
        public string MediaType { get; set; }

        [JsonProperty("experienceContext")]
        public string ExperienceContext { get; set; }

        [JsonProperty("productType")]
        public string ProductType { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }
    }

    public class FluffyTelemetry
    {
        [JsonProperty("fguid")]
        public Guid Fguid { get; set; }

        [JsonProperty("mediaId")]
        public Guid MediaId { get; set; }
    }
}