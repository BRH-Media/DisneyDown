using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DisneyDown.Common.API.Schemas.PageSchemas
{
    public partial class EntityPageInformation
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
        public Page Page { get; set; }
    }

    public class Page
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("deeplinkId", NullValueHandling = NullValueHandling.Ignore)]
        public string DeeplinkId { get; set; }

        [JsonProperty("style", NullValueHandling = NullValueHandling.Ignore)]
        public PageStyle Style { get; set; }

        [JsonProperty("visuals", NullValueHandling = NullValueHandling.Ignore)]
        public PageVisuals Visuals { get; set; }

        [JsonProperty("actions", NullValueHandling = NullValueHandling.Ignore)]
        public Action[] Actions { get; set; }

        [JsonProperty("containers", NullValueHandling = NullValueHandling.Ignore)]
        public Container[] Containers { get; set; }

        [JsonProperty("infoBlock", NullValueHandling = NullValueHandling.Ignore)]
        public string InfoBlock { get; set; }

        [JsonProperty("personalization", NullValueHandling = NullValueHandling.Ignore)]
        public Personalization Personalization { get; set; }

        [JsonProperty("refresh", NullValueHandling = NullValueHandling.Ignore)]
        public PageRefresh Refresh { get; set; }
    }

    public class Action
    {
        [JsonProperty("resourceId", NullValueHandling = NullValueHandling.Ignore)]
        public string ResourceId { get; set; }

        [JsonProperty("contentType", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentType { get; set; }

        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public Option[] Options { get; set; }

        [JsonProperty("availId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? AvailId { get; set; }

        [JsonProperty("deeplinkId", NullValueHandling = NullValueHandling.Ignore)]
        public string DeeplinkId { get; set; }

        [JsonProperty("legacyPartnerFeed", NullValueHandling = NullValueHandling.Ignore)]
        public PartnerFeed LegacyPartnerFeed { get; set; }

        [JsonProperty("partnerFeed", NullValueHandling = NullValueHandling.Ignore)]
        public PartnerFeed PartnerFeed { get; set; }

        [JsonProperty("upNextId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? UpNextId { get; set; }

        [JsonProperty("internalTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalTitle { get; set; }

        [JsonProperty("visuals", NullValueHandling = NullValueHandling.Ignore)]
        public ActionVisuals Visuals { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("infoBlock", NullValueHandling = NullValueHandling.Ignore)]
        public string InfoBlock { get; set; }

        [JsonProperty("contentId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? ContentId { get; set; }
    }

    public class PartnerFeed
    {
        [JsonProperty("dmcContentId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? DmcContentId { get; set; }
    }

    public class Option
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("displayText", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayText { get; set; }

        [JsonProperty("infoBlock", NullValueHandling = NullValueHandling.Ignore)]
        public string InfoBlock { get; set; }
    }

    public class ActionVisuals
    {
        [JsonProperty("displayText", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayText { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("artwork", NullValueHandling = NullValueHandling.Ignore)]
        public Artwork Artwork { get; set; }
    }

    public class Artwork
    {
        [JsonProperty("standard", NullValueHandling = NullValueHandling.Ignore)]
        public Standard Standard { get; set; }

        [JsonProperty("mono", NullValueHandling = NullValueHandling.Ignore)]
        public Mono Mono { get; set; }

        [JsonProperty("up_next", NullValueHandling = NullValueHandling.Ignore)]
        public Collection UpNext { get; set; }

        [JsonProperty("partner", NullValueHandling = NullValueHandling.Ignore)]
        public Partner Partner { get; set; }

        [JsonProperty("unauthenticated", NullValueHandling = NullValueHandling.Ignore)]
        public Collection Unauthenticated { get; set; }

        [JsonProperty("center", NullValueHandling = NullValueHandling.Ignore)]
        public Center Center { get; set; }

        [JsonProperty("tile", NullValueHandling = NullValueHandling.Ignore)]
        public Details Tile { get; set; }

        [JsonProperty("hero", NullValueHandling = NullValueHandling.Ignore)]
        public Hero Hero { get; set; }

        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
        public Details Details { get; set; }

        [JsonProperty("collection", NullValueHandling = NullValueHandling.Ignore)]
        public Collection Collection { get; set; }
    }

    public class Center
    {
        [JsonProperty("title_treatment", NullValueHandling = NullValueHandling.Ignore)]
        public BackgroundClass TitleTreatment { get; set; }
    }

    public class BackgroundClass
    {
        [JsonProperty("1.78", NullValueHandling = NullValueHandling.Ignore)]
        public The178 The178 { get; set; }
    }

    public class The178
    {
        [JsonProperty("imageId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? ImageId { get; set; }
    }

    public class Collection
    {
        [JsonProperty("background", NullValueHandling = NullValueHandling.Ignore)]
        public BackgroundClass Background { get; set; }
    }

    public class Details
    {
        [JsonProperty("background", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, The178> Background { get; set; }
    }

    public class Hero
    {
        [JsonProperty("background", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, The178> Background { get; set; }

        [JsonProperty("title_treatment", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, The178> TitleTreatment { get; set; }
    }

    public class Mono
    {
        [JsonProperty("title_treatment", NullValueHandling = NullValueHandling.Ignore)]
        public MonoTitleTreatment TitleTreatment { get; set; }
    }

    public class MonoTitleTreatment
    {
        [JsonProperty("3.32", NullValueHandling = NullValueHandling.Ignore)]
        public The178 The332 { get; set; }
    }

    public class Partner
    {
        [JsonProperty("thumbnail", NullValueHandling = NullValueHandling.Ignore)]
        public BackgroundClass Thumbnail { get; set; }

        [JsonProperty("tile", NullValueHandling = NullValueHandling.Ignore)]
        public BackgroundClass Tile { get; set; }
    }

    public class Standard
    {
        [JsonProperty("background", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, The178> Background { get; set; }

        [JsonProperty("tile", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, The178> Tile { get; set; }

        [JsonProperty("title_treatment", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, The178> TitleTreatment { get; set; }
    }

    public class Container
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Id { get; set; }

        [JsonProperty("style", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerStyle Style { get; set; }

        [JsonProperty("actions", NullValueHandling = NullValueHandling.Ignore)]
        public object[] Actions { get; set; }

        [JsonProperty("visuals", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerVisuals Visuals { get; set; }

        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public object[] Items { get; set; }

        [JsonProperty("infoBlock", NullValueHandling = NullValueHandling.Ignore)]
        public string InfoBlock { get; set; }

        [JsonProperty("pagination", NullValueHandling = NullValueHandling.Ignore)]
        public Pagination Pagination { get; set; }

        [JsonProperty("refresh", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerRefresh Refresh { get; set; }

        [JsonProperty("params", NullValueHandling = NullValueHandling.Ignore)]
        public Params Params { get; set; }
    }

    public class Pagination
    {
        [JsonProperty("hasMore", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasMore { get; set; }

        [JsonProperty("hasPrev", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasPrev { get; set; }

        [JsonProperty("currentOffset", NullValueHandling = NullValueHandling.Ignore)]
        public long? CurrentOffset { get; set; }
    }

    public class Params
    {
        [JsonProperty("entity_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? EntityId { get; set; }

        [JsonProperty("entity_type", NullValueHandling = NullValueHandling.Ignore)]
        public string EntityType { get; set; }

        [JsonProperty("setStyle", NullValueHandling = NullValueHandling.Ignore)]
        public string SetStyle { get; set; }
    }

    public class ContainerRefresh
    {
        [JsonProperty("policy", NullValueHandling = NullValueHandling.Ignore)]
        public string Policy { get; set; }
    }

    public class ContainerStyle
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("fallback", NullValueHandling = NullValueHandling.Ignore)]
        public string Fallback { get; set; }

        [JsonProperty("layout", NullValueHandling = NullValueHandling.Ignore)]
        public string Layout { get; set; }
    }

    public class ContainerVisuals
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public Description Description { get; set; }

        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public Duration Duration { get; set; }

        [JsonProperty("releaseYearRange", NullValueHandling = NullValueHandling.Ignore)]
        public VisualsReleaseYearRange ReleaseYearRange { get; set; }

        [JsonProperty("genres", NullValueHandling = NullValueHandling.Ignore)]
        public VisualsGenres Genres { get; set; }

        [JsonProperty("audioVisual", NullValueHandling = NullValueHandling.Ignore)]
        public VisualsAudioVisual AudioVisual { get; set; }

        [JsonProperty("ratingInfo", NullValueHandling = NullValueHandling.Ignore)]
        public VisualsRatingInfo RatingInfo { get; set; }

        [JsonProperty("credits", NullValueHandling = NullValueHandling.Ignore)]
        public Credit[] Credits { get; set; }
    }

    public class VisualsAudioVisual
    {
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty("flags", NullValueHandling = NullValueHandling.Ignore)]
        public Flag[] Flags { get; set; }
    }

    public class Flag
    {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("imageId", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageId { get; set; }

        [JsonProperty("tts", NullValueHandling = NullValueHandling.Ignore)]
        public string Tts { get; set; }
    }

    public class Credit
    {
        [JsonProperty("heading", NullValueHandling = NullValueHandling.Ignore)]
        public string Heading { get; set; }

        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public Item[] Items { get; set; }
    }

    public class Item
    {
        [JsonProperty("displayText", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayText { get; set; }
    }

    public class Description
    {
        [JsonProperty("brief", NullValueHandling = NullValueHandling.Ignore)]
        public string Brief { get; set; }

        [JsonProperty("medium", NullValueHandling = NullValueHandling.Ignore)]
        public string Medium { get; set; }

        [JsonProperty("full", NullValueHandling = NullValueHandling.Ignore)]
        public string Full { get; set; }
    }

    public class Duration
    {
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty("runtimeMs", NullValueHandling = NullValueHandling.Ignore)]
        public long? RuntimeMs { get; set; }
    }

    public class VisualsGenres
    {
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Values { get; set; }
    }

    public class VisualsRatingInfo
    {
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
        public Rating Rating { get; set; }

        [JsonProperty("advisories", NullValueHandling = NullValueHandling.Ignore)]
        public object[] Advisories { get; set; }
    }

    public class Rating
    {
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("imageId", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageId { get; set; }
    }

    public class VisualsReleaseYearRange
    {
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty("startYear", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? StartYear { get; set; }
    }

    public class Personalization
    {
        [JsonProperty("pid", NullValueHandling = NullValueHandling.Ignore)]
        public string Pid { get; set; }

        [JsonProperty("userState", NullValueHandling = NullValueHandling.Ignore)]
        public UserState UserState { get; set; }
    }

    public class UserState
    {
        [JsonProperty("inWatchlist", NullValueHandling = NullValueHandling.Ignore)]
        public bool? InWatchlist { get; set; }

        [JsonProperty("progress", NullValueHandling = NullValueHandling.Ignore)]
        public Progress Progress { get; set; }
    }

    public class Progress
    {
        [JsonProperty("progressPercentage", NullValueHandling = NullValueHandling.Ignore)]
        public long? ProgressPercentage { get; set; }

        [JsonProperty("secondsRemaining", NullValueHandling = NullValueHandling.Ignore)]
        public long? SecondsRemaining { get; set; }
    }

    public class PageRefresh
    {
        [JsonProperty("policy", NullValueHandling = NullValueHandling.Ignore)]
        public string Policy { get; set; }

        [JsonProperty("ttlSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public long? TtlSeconds { get; set; }
    }

    public class PageStyle
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("fallback", NullValueHandling = NullValueHandling.Ignore)]
        public string Fallback { get; set; }
    }

    public class PageVisuals
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public Description Description { get; set; }

        [JsonProperty("artwork", NullValueHandling = NullValueHandling.Ignore)]
        public Artwork Artwork { get; set; }

        [JsonProperty("metastringParts", NullValueHandling = NullValueHandling.Ignore)]
        public MetastringParts MetastringParts { get; set; }
    }

    public class MetastringParts
    {
        [JsonProperty("genres", NullValueHandling = NullValueHandling.Ignore)]
        public MetastringPartsGenres Genres { get; set; }

        [JsonProperty("audioVisual", NullValueHandling = NullValueHandling.Ignore)]
        public MetastringPartsAudioVisual AudioVisual { get; set; }

        [JsonProperty("ratingInfo", NullValueHandling = NullValueHandling.Ignore)]
        public MetastringPartsRatingInfo RatingInfo { get; set; }

        [JsonProperty("releaseYearRange", NullValueHandling = NullValueHandling.Ignore)]
        public MetastringPartsReleaseYearRange ReleaseYearRange { get; set; }

        [JsonProperty("runtime", NullValueHandling = NullValueHandling.Ignore)]
        public Runtime Runtime { get; set; }
    }

    public class MetastringPartsAudioVisual
    {
        [JsonProperty("flags", NullValueHandling = NullValueHandling.Ignore)]
        public Flag[] Flags { get; set; }
    }

    public class MetastringPartsGenres
    {
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Values { get; set; }
    }

    public class MetastringPartsRatingInfo
    {
        [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
        public Rating Rating { get; set; }

        [JsonProperty("advisories", NullValueHandling = NullValueHandling.Ignore)]
        public object[] Advisories { get; set; }
    }

    public class MetastringPartsReleaseYearRange
    {
        [JsonProperty("startYear", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? StartYear { get; set; }
    }

    public class Runtime
    {
        [JsonProperty("runtimeMs", NullValueHandling = NullValueHandling.Ignore)]
        public long? RuntimeMs { get; set; }
    }

    public partial class EntityPageInformation
    {
        public static EntityPageInformation FromJson(string json) => JsonConvert.DeserializeObject<EntityPageInformation>(json, ApiJsonConverter.Settings);
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

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}