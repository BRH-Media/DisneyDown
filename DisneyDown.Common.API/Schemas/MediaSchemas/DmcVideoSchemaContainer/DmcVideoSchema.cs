using Newtonsoft.Json;
using System;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class DmcVideoSchema
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        public static DmcVideoSchema FromJson(string json) => JsonConvert.DeserializeObject<DmcVideoSchema>(json, Converter.Settings);
    }

    public class Data
    {
        [JsonProperty("DmcVideo")]
        public DmcVideo DmcVideo { get; set; }
    }

    public class DmcVideo
    {
        [JsonProperty("video")]
        public Video Video { get; set; }
    }

    public class Video
    {
        [JsonProperty("badging")]
        public object Badging { get; set; }

        [JsonProperty("callToAction")]
        public object CallToAction { get; set; }

        [JsonProperty("contentId")]
        public Guid ContentId { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("currentAvailability")]
        public CurrentAvailability CurrentAvailability { get; set; }

        [JsonProperty("encodedSeriesId")]
        public string EncodedSeriesId { get; set; }

        [JsonProperty("episodeNumber")]
        public object EpisodeNumber { get; set; }

        [JsonProperty("episodeSequenceNumber")]
        public long EpisodeSequenceNumber { get; set; }

        [JsonProperty("episodeSeriesSequenceNumber")]
        public long EpisodeSeriesSequenceNumber { get; set; }

        [JsonProperty("event")]
        public object Event { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; }

        [JsonProperty("groups")]
        public Group[] Groups { get; set; }

        [JsonProperty("internalTitle")]
        public string InternalTitle { get; set; }

        [JsonProperty("images")]
        public Image[] Images { get; set; }

        [JsonProperty("labels")]
        public object[] Labels { get; set; }

        [JsonProperty("mediaMetadata")]
        public MediaMetadata MediaMetadata { get; set; }

        [JsonProperty("mediaRights")]
        public MediaRights MediaRights { get; set; }

        [JsonProperty("meta")]
        public object Meta { get; set; }

        [JsonProperty("milestones")]
        public Milestone[] Milestones { get; set; }

        [JsonProperty("originalLanguage")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("participants")]
        public Participant[] Participants { get; set; }

        [JsonProperty("programId")]
        public Guid ProgramId { get; set; }

        [JsonProperty("programType")]
        public string ProgramType { get; set; }

        [JsonProperty("ratings")]
        public Rating[] Ratings { get; set; }

        [JsonProperty("releases")]
        public Release[] Releases { get; set; }

        [JsonProperty("seasonId")]
        public Guid SeasonId { get; set; }

        [JsonProperty("seasonSequenceNumber")]
        public long SeasonSequenceNumber { get; set; }

        [JsonProperty("seriesId")]
        public Guid SeriesId { get; set; }

        [JsonProperty("seriesType")]
        public string SeriesType { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("targetLanguage")]
        public string TargetLanguage { get; set; }

        [JsonProperty("texts")]
        public Text[] Texts { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("typedGenres")]
        public object[] TypedGenres { get; set; }

        [JsonProperty("videoArt")]
        public object[] VideoArt { get; set; }

        [JsonProperty("videoId")]
        public Guid VideoId { get; set; }
    }

    public class CurrentAvailability
    {
        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("kidsMode")]
        public bool KidsMode { get; set; }
    }

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

    public class ParentRef
    {
        [JsonProperty("encodedSeriesId")]
        public string EncodedSeriesId { get; set; }

        [JsonProperty("programId")]
        public Guid ProgramId { get; set; }

        [JsonProperty("seasonId")]
        public Guid SeasonId { get; set; }

        [JsonProperty("seriesId")]
        public Guid SeriesId { get; set; }
    }

    public class Group
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("partnerGroupId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PartnerGroupId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Image
    {
        [JsonProperty("aspectRatio")]
        public double AspectRatio { get; set; }

        [JsonProperty("masterHeight")]
        public long MasterHeight { get; set; }

        [JsonProperty("masterId")]
        public string MasterId { get; set; }

        [JsonProperty("masterWidth")]
        public long MasterWidth { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; }

        [JsonProperty("sourceEntity")]
        public string SourceEntity { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public class MediaMetadata
    {
        [JsonProperty("activeAspectRatio")]
        public double ActiveAspectRatio { get; set; }

        [JsonProperty("audioTracks")]
        public AudioTrack[] AudioTracks { get; set; }

        [JsonProperty("captions")]
        public AudioTrack[] Captions { get; set; }

        [JsonProperty("facets")]
        public Facet[] Facets { get; set; }

        [JsonProperty("features")]
        public object[] Features { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("mediaId")]
        public Guid MediaId { get; set; }

        [JsonProperty("phase")]
        public string Phase { get; set; }

        [JsonProperty("playbackUrls")]
        public PlaybackUrl[] PlaybackUrls { get; set; }

        [JsonProperty("productType")]
        public string ProductType { get; set; }

        [JsonProperty("runtimeMillis")]
        public long RuntimeMillis { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class AudioTrack
    {
        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Features { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("renditionName")]
        public string RenditionName { get; set; }

        [JsonProperty("trackType")]
        public string TrackType { get; set; }
    }

    public class Facet
    {
        [JsonProperty("activeAspectRatio")]
        public double ActiveAspectRatio { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public class PlaybackUrl
    {
        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("params")]
        public Param[] Params { get; set; }
    }

    public class Param
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class MediaRights
    {
        [JsonProperty("violations")]
        public object[] Violations { get; set; }

        [JsonProperty("downloadBlocked")]
        public bool DownloadBlocked { get; set; }

        [JsonProperty("pconBlocked")]
        public bool PconBlocked { get; set; }

        [JsonProperty("rewind")]
        public bool Rewind { get; set; }
    }

    public class Milestone
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("milestoneTime")]
        public MilestoneTime[] MilestoneTime { get; set; }

        [JsonProperty("milestoneType")]
        public string MilestoneType { get; set; }
    }

    public class MilestoneTime
    {
        [JsonProperty("startMillis")]
        public long StartMillis { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Participant
    {
        [JsonProperty("characterDetails")]
        public CharacterDetails CharacterDetails { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("participantId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ParticipantId { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("sortName")]
        public string SortName { get; set; }
    }

    public class CharacterDetails
    {
        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("characterId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CharacterId { get; set; }

        [JsonProperty("language")]
        public object Language { get; set; }
    }

    public class Rating
    {
        [JsonProperty("advisories")]
        public object[] Advisories { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("filingNumber")]
        public object FilingNumber { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Release
    {
        [JsonProperty("releaseDate")]
        public DateTimeOffset ReleaseDate { get; set; }

        [JsonProperty("releaseOrg")]
        public object ReleaseOrg { get; set; }

        [JsonProperty("releaseType")]
        public string ReleaseType { get; set; }

        [JsonProperty("releaseYear")]
        public long ReleaseYear { get; set; }

        [JsonProperty("territory")]
        public object Territory { get; set; }
    }

    public class Tag
    {
        [JsonProperty("displayName")]
        public object DisplayName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Text
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("sourceEntity")]
        public string SourceEntity { get; set; }

        [JsonProperty("targetEntity")]
        public string TargetEntity { get; set; }
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (long.TryParse(value, out var l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
        }
    }
}