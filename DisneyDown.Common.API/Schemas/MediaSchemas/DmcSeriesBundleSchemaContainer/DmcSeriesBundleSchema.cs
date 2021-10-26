using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class DmcSeriesBundleSchema
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        public static DmcSeriesBundleSchema FromJson(string json) => JsonConvert.DeserializeObject<DmcSeriesBundleSchema>(json, Converter.Settings);
    }

    public class Data
    {
        [JsonProperty("DmcSeriesBundle")]
        public DmcSeriesBundle DmcSeriesBundle { get; set; }
    }

    public class DmcSeriesBundle
    {
        [JsonProperty("episodes")]
        public Episodes Episodes { get; set; }

        [JsonProperty("extras")]
        public Episodes Extras { get; set; }

        [JsonProperty("promoLabels")]
        public object[] PromoLabels { get; set; }

        [JsonProperty("related")]
        public Related Related { get; set; }

        [JsonProperty("seasons")]
        public Seasons Seasons { get; set; }

        [JsonProperty("series")]
        public DmcSeriesBundleSeries Series { get; set; }
    }

    public class Episodes
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("videos")]
        public Video[] Videos { get; set; }
    }

    public class Meta
    {
        [JsonProperty("hits")]
        public long Hits { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("page_size")]
        public long PageSize { get; set; }
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

        [JsonProperty("event")]
        public object Event { get; set; }

        [JsonProperty("encodedSeriesId")]
        public string EncodedSeriesId { get; set; }

        [JsonProperty("episodeNumber")]
        public object EpisodeNumber { get; set; }

        [JsonProperty("episodeSequenceNumber")]
        public long EpisodeSequenceNumber { get; set; }

        [JsonProperty("episodeSeriesSequenceNumber")]
        public long EpisodeSeriesSequenceNumber { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; }

        [JsonProperty("groups")]
        public Group[] Groups { get; set; }

        [JsonProperty("internalTitle")]
        public string InternalTitle { get; set; }

        [JsonProperty("image")]
        public VideoImage Image { get; set; }

        [JsonProperty("labels")]
        public object[] Labels { get; set; }

        [JsonProperty("mediaMetadata")]
        public VideoMediaMetadata MediaMetadata { get; set; }

        [JsonProperty("meta")]
        public object Meta { get; set; }

        [JsonProperty("mediaRights")]
        public VideoMediaRights MediaRights { get; set; }

        [JsonProperty("milestone")]
        public Dictionary<string, Milestone[]> Milestone { get; set; }

        [JsonProperty("originalLanguage")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("participant")]
        public Participant Participant { get; set; }

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

        [JsonProperty("text")]
        public VideoText Text { get; set; }

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
        public Guid? SeasonId { get; set; }

        [JsonProperty("seriesId")]
        public Guid? SeriesId { get; set; }
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

    public class VideoImage
    {
        [JsonProperty("tile")]
        public Dictionary<string, BackgroundDetail> Tile { get; set; }

        [JsonProperty("title_treatment")]
        public TitleTreatment TitleTreatment { get; set; }

        [JsonProperty("title_treatment_centered")]
        public TitleTreatment TitleTreatmentCentered { get; set; }

        [JsonProperty("background_up_next")]
        public BackgroundUpNext BackgroundUpNext { get; set; }

        [JsonProperty("background_details")]
        public Dictionary<string, BackgroundDetail> BackgroundDetails { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }
    }

    public class BackgroundDetail
    {
        [JsonProperty("series")]
        public BackgroundDetailSeries Series { get; set; }
    }

    public class BackgroundDetailSeries
    {
        [JsonProperty("default")]
        public PurpleDefault Default { get; set; }
    }

    public class PurpleDefault
    {
        [JsonProperty("masterId")]
        public string MasterId { get; set; }

        [JsonProperty("masterWidth")]
        public long MasterWidth { get; set; }

        [JsonProperty("masterHeight")]
        public long MasterHeight { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public class BackgroundUpNext
    {
        [JsonProperty("1.78")]
        public BackgroundDetail The178 { get; set; }
    }

    public class Thumbnail
    {
        [JsonProperty("1.78")]
        public BackgroundValue The178 { get; set; }
    }

    public class BackgroundValue
    {
        [JsonProperty("program")]
        public BackgroundDetailSeries Program { get; set; }
    }

    public class TitleTreatment
    {
        [JsonProperty("1.78")]
        public BackgroundDetailValue The178 { get; set; }
    }

    public class BackgroundDetailValue
    {
        [JsonProperty("series", NullValueHandling = NullValueHandling.Ignore)]
        public BackgroundDetailSeries Series { get; set; }

        [JsonProperty("program", NullValueHandling = NullValueHandling.Ignore)]
        public BackgroundDetailSeries Program { get; set; }
    }

    public class VideoMediaMetadata
    {
        [JsonProperty("activeAspectRatio")]
        public double ActiveAspectRatio { get; set; }

        [JsonProperty("audioTracks")]
        public AudioTrack[] AudioTracks { get; set; }

        [JsonProperty("captions")]
        public AudioTrack[] Captions { get; set; }

        [JsonProperty("facets")]
        public PurpleFacet[] Facets { get; set; }

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

    public class PurpleFacet
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

    public class VideoMediaRights
    {
        [JsonProperty("violations", NullValueHandling = NullValueHandling.Ignore)]
        public object[] Violations { get; set; }

        [JsonProperty("downloadBlocked")]
        public bool DownloadBlocked { get; set; }

        [JsonProperty("pconBlocked")]
        public bool PconBlocked { get; set; }

        [JsonProperty("rewind", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Rewind { get; set; }
    }

    public class Milestone
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("milestoneTime")]
        public MilestoneTime[] MilestoneTime { get; set; }
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
        [JsonProperty("Actor")]
        public Actor[] Actor { get; set; }

        [JsonProperty("Created By")]
        public Actor[] CreatedBy { get; set; }
    }

    public class Actor
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
    }

    public class Rating
    {
        [JsonProperty("advisories")]
        public object[] Advisories { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

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

    public class VideoText
    {
        [JsonProperty("description")]
        public PurpleDescription Description { get; set; }

        [JsonProperty("title")]
        public PurpleTitle Title { get; set; }
    }

    public class PurpleDescription
    {
        [JsonProperty("medium")]
        public PurpleBrief Medium { get; set; }

        [JsonProperty("brief")]
        public PurpleBrief Brief { get; set; }

        [JsonProperty("full")]
        public PurpleBrief Full { get; set; }
    }

    public class PurpleBrief
    {
        [JsonProperty("program", NullValueHandling = NullValueHandling.Ignore)]
        public BriefSeries Program { get; set; }

        [JsonProperty("series", NullValueHandling = NullValueHandling.Ignore)]
        public BriefSeries Series { get; set; }
    }

    public class BriefSeries
    {
        [JsonProperty("default")]
        public FluffyDefault Default { get; set; }
    }

    public class FluffyDefault
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("sourceEntity")]
        public string SourceEntity { get; set; }
    }

    public class PurpleTitle
    {
        [JsonProperty("slug")]
        public PurpleBrief Slug { get; set; }

        [JsonProperty("full")]
        public PurpleBrief Full { get; set; }
    }

    public class Related
    {
        [JsonProperty("experimentToken")]
        public string ExperimentToken { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }

    public class Item
    {
        [JsonProperty("badging")]
        public object Badging { get; set; }

        [JsonProperty("callToAction")]
        public object CallToAction { get; set; }

        [JsonProperty("contentId")]
        public Guid ContentId { get; set; }

        [JsonProperty("contentType", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentType { get; set; }

        [JsonProperty("currentAvailability")]
        public CurrentAvailability CurrentAvailability { get; set; }

        [JsonProperty("encodedSeriesId")]
        public string EncodedSeriesId { get; set; }

        [JsonProperty("episodeNumber")]
        public object EpisodeNumber { get; set; }

        [JsonProperty("episodeSequenceNumber")]
        public object EpisodeSequenceNumber { get; set; }

        [JsonProperty("episodeSeriesSequenceNumber")]
        public object EpisodeSeriesSequenceNumber { get; set; }

        [JsonProperty("event")]
        public object Event { get; set; }

        [JsonProperty("family", NullValueHandling = NullValueHandling.Ignore)]
        public Family Family { get; set; }

        [JsonProperty("groups", NullValueHandling = NullValueHandling.Ignore)]
        public Group[] Groups { get; set; }

        [JsonProperty("internalTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalTitle { get; set; }

        [JsonProperty("image")]
        public ItemImage Image { get; set; }

        [JsonProperty("mediaMetadata", NullValueHandling = NullValueHandling.Ignore)]
        public ItemMediaMetadata MediaMetadata { get; set; }

        [JsonProperty("mediaRights")]
        public VideoMediaRights MediaRights { get; set; }

        [JsonProperty("originalLanguage", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalLanguage { get; set; }

        [JsonProperty("programId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? ProgramId { get; set; }

        [JsonProperty("programType", NullValueHandling = NullValueHandling.Ignore)]
        public string ProgramType { get; set; }

        [JsonProperty("seasonId")]
        public object SeasonId { get; set; }

        [JsonProperty("seasonSequenceNumber")]
        public object SeasonSequenceNumber { get; set; }

        [JsonProperty("seriesId")]
        public Guid? SeriesId { get; set; }

        [JsonProperty("seriesType")]
        public string SeriesType { get; set; }

        [JsonProperty("text")]
        public VideoText Text { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("targetLanguage", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetLanguage { get; set; }

        [JsonProperty("ratings")]
        public Rating[] Ratings { get; set; }

        [JsonProperty("releases")]
        public Release[] Releases { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("videoArt")]
        public VideoArt[] VideoArt { get; set; }

        [JsonProperty("videoId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? VideoId { get; set; }

        [JsonProperty("textExperienceId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? TextExperienceId { get; set; }
    }

    public class ItemImage
    {
        [JsonProperty("title_treatment")]
        public TitleTreatment TitleTreatment { get; set; }

        [JsonProperty("tile")]
        public Dictionary<string, BackgroundDetailValue> Tile { get; set; }

        [JsonProperty("background", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, BackgroundValue> Background { get; set; }

        [JsonProperty("title_treatment_centered")]
        public TitleTreatment TitleTreatmentCentered { get; set; }

        [JsonProperty("background_up_next", NullValueHandling = NullValueHandling.Ignore)]
        public TitleTreatment BackgroundUpNext { get; set; }

        [JsonProperty("background_details", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, BackgroundDetailValue> BackgroundDetails { get; set; }
    }

    public class ItemMediaMetadata
    {
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

    public class VideoArt
    {
        [JsonProperty("mediaMetadata")]
        public VideoArtMediaMetadata MediaMetadata { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; }
    }

    public class VideoArtMediaMetadata
    {
        [JsonProperty("urls")]
        public Url[] Urls { get; set; }
    }

    public class Url
    {
        [JsonProperty("url")]
        public Uri UrlUrl { get; set; }
    }

    public class Seasons
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("seasons")]
        public Season[] SeasonsSeasons { get; set; }
    }

    public class Season
    {
        [JsonProperty("downloadableEpisodes")]
        public Guid[] DownloadableEpisodes { get; set; }

        [JsonProperty("episodes_meta")]
        public Meta EpisodesMeta { get; set; }

        [JsonProperty("encodedSeriesId")]
        public string EncodedSeriesId { get; set; }

        [JsonProperty("labels")]
        public object[] Labels { get; set; }

        [JsonProperty("mediaRights")]
        public VideoMediaRights MediaRights { get; set; }

        [JsonProperty("releases")]
        public Release[] Releases { get; set; }

        [JsonProperty("seasonId")]
        public Guid SeasonId { get; set; }

        [JsonProperty("seasonNumber")]
        public object SeasonNumber { get; set; }

        [JsonProperty("seasonSequenceNumber")]
        public long SeasonSequenceNumber { get; set; }

        [JsonProperty("seriesId")]
        public Guid SeriesId { get; set; }

        [JsonProperty("seriesType")]
        public string SeriesType { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("text")]
        public SeasonText Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class SeasonText
    {
    }

    public class DmcSeriesBundleSeries
    {
        [JsonProperty("badging")]
        public object Badging { get; set; }

        [JsonProperty("callToAction")]
        public object CallToAction { get; set; }

        [JsonProperty("contentId")]
        public Guid ContentId { get; set; }

        [JsonProperty("currentAvailability")]
        public CurrentAvailability CurrentAvailability { get; set; }

        [JsonProperty("encodedSeriesId")]
        public string EncodedSeriesId { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; }

        [JsonProperty("image")]
        public SeriesImage Image { get; set; }

        [JsonProperty("labels")]
        public object[] Labels { get; set; }

        [JsonProperty("mediaMetadata")]
        public SeriesMediaMetadata MediaMetadata { get; set; }

        [JsonProperty("mediaRights")]
        public SeriesMediaRights MediaRights { get; set; }

        [JsonProperty("participant")]
        public Participant Participant { get; set; }

        [JsonProperty("seasonsMaxYear")]
        public long SeasonsMaxYear { get; set; }

        [JsonProperty("seasonsMinYear")]
        public long SeasonsMinYear { get; set; }

        [JsonProperty("seriesId")]
        public Guid SeriesId { get; set; }

        [JsonProperty("seriesType")]
        public string SeriesType { get; set; }

        [JsonProperty("text")]
        public SeriesText Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("typedGenres")]
        public TypedGenre[] TypedGenres { get; set; }

        [JsonProperty("ratings")]
        public Rating[] Ratings { get; set; }

        [JsonProperty("releases")]
        public Release[] Releases { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("videoArt")]
        public object[] VideoArt { get; set; }
    }

    public class SeriesImage
    {
        [JsonProperty("tile")]
        public Dictionary<string, BackgroundDetail> Tile { get; set; }

        [JsonProperty("title_treatment")]
        public BackgroundUpNext TitleTreatment { get; set; }

        [JsonProperty("title_treatment_centered")]
        public BackgroundUpNext TitleTreatmentCentered { get; set; }

        [JsonProperty("background_up_next")]
        public BackgroundUpNext BackgroundUpNext { get; set; }

        [JsonProperty("background_details")]
        public Dictionary<string, BackgroundDetail> BackgroundDetails { get; set; }
    }

    public class SeriesMediaMetadata
    {
        [JsonProperty("facets")]
        public FluffyFacet[] Facets { get; set; }
    }

    public class FluffyFacet
    {
        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public class SeriesMediaRights
    {
        [JsonProperty("downloadBlocked")]
        public bool DownloadBlocked { get; set; }

        [JsonProperty("pconBlocked")]
        public bool PconBlocked { get; set; }
    }

    public class SeriesText
    {
        [JsonProperty("description")]
        public FluffyDescription Description { get; set; }

        [JsonProperty("title")]
        public FluffyTitle Title { get; set; }
    }

    public class FluffyDescription
    {
        [JsonProperty("brief")]
        public FluffyBrief Brief { get; set; }

        [JsonProperty("full")]
        public FluffyBrief Full { get; set; }

        [JsonProperty("medium")]
        public FluffyBrief Medium { get; set; }
    }

    public class FluffyBrief
    {
        [JsonProperty("series")]
        public BriefSeries Series { get; set; }
    }

    public class FluffyTitle
    {
        [JsonProperty("slug")]
        public FluffyBrief Slug { get; set; }

        [JsonProperty("full")]
        public FluffyBrief Full { get; set; }
    }

    public class TypedGenre
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("partnerId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PartnerId { get; set; }
    }
}