using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class TentacledEndpoints
    {
        [JsonProperty("addToWatchlist")]
        public Endpoint AddToWatchlist { get; set; }

        [JsonProperty("autoSuggestPersisted")]
        public Endpoint AutoSuggestPersisted { get; set; }

        [JsonProperty("bookmarks")]
        public Endpoint Bookmarks { get; set; }

        [JsonProperty("bookmarksPersisted")]
        public Endpoint BookmarksPersisted { get; set; }

        [JsonProperty("deleteFromWatchlist")]
        public Endpoint DeleteFromWatchlist { get; set; }

        [JsonProperty("deleteItemFromWatchlist")]
        public Endpoint DeleteItemFromWatchlist { get; set; }

        [JsonProperty("dmcVideos")]
        public Endpoint DmcVideos { get; set; }

        [JsonProperty("getAiring")]
        public Endpoint GetAiring { get; set; }

        [JsonProperty("getAvatar")]
        public Endpoint GetAvatar { get; set; }

        [JsonProperty("getAvatars")]
        public Endpoint GetAvatars { get; set; }

        [JsonProperty("getCWSeason")]
        public Endpoint GetCwSeason { get; set; }

        [JsonProperty("getCWSeasonWithBookmark")]
        public Endpoint GetCwSeasonWithBookmark { get; set; }

        [JsonProperty("getCWSeries")]
        public Endpoint GetCwSeries { get; set; }

        [JsonProperty("getCWSeriesWithBookmark")]
        public Endpoint GetCwSeriesWithBookmark { get; set; }

        [JsonProperty("getCWSet")]
        public Endpoint GetCwSet { get; set; }

        [JsonProperty("getCWSetWithBookmark")]
        public Endpoint GetCwSetWithBookmark { get; set; }

        [JsonProperty("getCWVideo")]
        public Endpoint GetCwVideo { get; set; }

        [JsonProperty("getCWVideoWithBookmark")]
        public Endpoint GetCwVideoWithBookmark { get; set; }

        [JsonProperty("getCollection")]
        public Endpoint GetCollection { get; set; }

        [JsonProperty("getCollectionByGroupId")]
        public Endpoint GetCollectionByGroupId { get; set; }

        [JsonProperty("getCompleteStandardCollection")]
        public Endpoint GetCompleteStandardCollection { get; set; }

        [JsonProperty("getCuratedSet")]
        public Endpoint GetCuratedSet { get; set; }

        [JsonProperty("getDictionaryLatest")]
        public Endpoint GetDictionaryLatest { get; set; }

        [JsonProperty("getDictionaryVersion")]
        public Endpoint GetDictionaryVersion { get; set; }

        [JsonProperty("getDmcEpisodes")]
        public Endpoint GetDmcEpisodes { get; set; }

        [JsonProperty("getDmcExtras")]
        public Endpoint GetDmcExtras { get; set; }

        [JsonProperty("getDmcProgramBundle")]
        public Endpoint GetDmcProgramBundle { get; set; }

        [JsonProperty("getDmcSeason")]
        public Endpoint GetDmcSeason { get; set; }

        [JsonProperty("getDmcSeasonByNumber")]
        public Endpoint GetDmcSeasonByNumber { get; set; }

        [JsonProperty("getDmcSeasons")]
        public Endpoint GetDmcSeasons { get; set; }

        [JsonProperty("getDmcSeries")]
        public Endpoint GetDmcSeries { get; set; }

        [JsonProperty("getDmcSeriesBundle")]
        public Endpoint GetDmcSeriesBundle { get; set; }

        [JsonProperty("getDmcSeriesMeta")]
        public Endpoint GetDmcSeriesMeta { get; set; }

        [JsonProperty("getDmcVideo")]
        public Endpoint GetDmcVideo { get; set; }

        [JsonProperty("getDmcVideoBundle")]
        public Endpoint GetDmcVideoBundle { get; set; }

        [JsonProperty("getDmcVideoMeta")]
        public Endpoint GetDmcVideoMeta { get; set; }

        [JsonProperty("getPersonalizedProgramBundle")]
        public Endpoint GetPersonalizedProgramBundle { get; set; }

        [JsonProperty("getPersonalizedProgramBundleWithBookmark")]
        public Endpoint GetPersonalizedProgramBundleWithBookmark { get; set; }

        [JsonProperty("getPersonalizedSeriesBundle")]
        public Endpoint GetPersonalizedSeriesBundle { get; set; }

        [JsonProperty("getPersonalizedSeriesBundleWithBookmark")]
        public Endpoint GetPersonalizedSeriesBundleWithBookmark { get; set; }

        [JsonProperty("getPersonalizedSet")]
        public Endpoint GetPersonalizedSet { get; set; }

        [JsonProperty("getRelatedItemsForSeries")]
        public Endpoint GetRelatedItemsForSeries { get; set; }

        [JsonProperty("getRelatedItemsForVideo")]
        public Endpoint GetRelatedItemsForVideo { get; set; }

        [JsonProperty("getSearchResults")]
        public Endpoint GetSearchResults { get; set; }

        [JsonProperty("getSet")]
        public Endpoint GetSet { get; set; }

        [JsonProperty("getSiteSearch")]
        public Endpoint GetSiteSearch { get; set; }

        [JsonProperty("getStandardCollection")]
        public Endpoint GetStandardCollection { get; set; }

        [JsonProperty("getUpNext")]
        public Endpoint GetUpNext { get; set; }

        [JsonProperty("putItemInWatchlist")]
        public Endpoint PutItemInWatchlist { get; set; }

        [JsonProperty("resourcesPersisted")]
        public ResourcesPersisted ResourcesPersisted { get; set; }

        [JsonProperty("search")]
        public Endpoint Search { get; set; }

        [JsonProperty("searchPersisted")]
        public Endpoint SearchPersisted { get; set; }

        [JsonProperty("searchSuggestions")]
        public ResourcesPersisted SearchSuggestions { get; set; }

        [JsonProperty("sportsData")]
        public Endpoint SportsData { get; set; }

        [JsonProperty("watchlist")]
        public Endpoint Watchlist { get; set; }

        [JsonProperty("watchlistPersisted")]
        public Endpoint WatchlistPersisted { get; set; }
    }
}