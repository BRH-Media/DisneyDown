using System;
using System.Collections.Generic;
using System.IO;
using DisneyDown.Common.API.Globals;
using DisneyDown.Common.Net;
using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using RestSharp;

// ReSharper disable RedundantIfElseBlock
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace DisneyDown.Common.API.Schemas
{
    public class ServiceInformation
    {
        [JsonProperty("application")]
        public Application Application { get; set; }

        [JsonProperty("commonHeaders")]
        public CommonHeaders CommonHeaders { get; set; }

        [JsonProperty("logger")]
        public Logger Logger { get; set; }

        [JsonProperty("services")]
        public Services Services { get; set; }

        public static ServiceInformation FromJson(string json) =>
            JsonConvert.DeserializeObject<ServiceInformation>(json, Converter.Settings);

        //retrieves the services JSON content from a URL or a file
        public static ServiceInformation GetServices(bool verbose = true)
        {
            //console verbosity setting
            ConsoleWriters.DisableAllOutput = !verbose;

            try
            {
                //directory verification
                if (!Directory.Exists(Path.GetDirectoryName(Strings.ServicesFile)))
                {
                    //create directory
                    Directory.CreateDirectory(Path.GetDirectoryName(Strings.ServicesFile) ?? @"cfg");
                }

                //is there a services JSON already present?
                if (File.Exists(Strings.ServicesFile))
                {
                    //alert user
                    ConsoleWriters.ConsoleWriteInfo($"Loading services catalog from file: {Strings.ServicesFile}");

                    //load it instead of downloading a new copy
                    var servicesJson = File.ReadAllText(Strings.ServicesFile);

                    //verification
                    if (!string.IsNullOrWhiteSpace(servicesJson))
                    {
                        //deserialisation
                        var services = FromJson(servicesJson);

                        //validation
                        if (services != null)
                        {
                            //return the result
                            return services;
                        }
                        else
                        {
                            //alert user
                            ConsoleWriters.ConsoleWriteError(@"Invalid services catalog: null object");
                        }
                    }
                    else
                    {
                        //delete the invalid file
                        File.Delete(Strings.ServicesFile);

                        //retry operations
                        return GetServices();
                    }
                }
                else
                {
                    //alert user
                    ConsoleWriters.ConsoleWriteInfo(@"Download new services catalog from Disney+");

                    //download a new copy
                    var servicesJson = ResourceGrab.GrabString(Strings.ServicesUrl);

                    //validation
                    if (!string.IsNullOrWhiteSpace(servicesJson))
                    {
                        //format the ugly JSON
                        var formattedJson = JsonUtil.JsonPrettify(servicesJson);

                        //save to a file
                        File.WriteAllText(Strings.ServicesFile, formattedJson);

                        //alert user
                        ConsoleWriters.ConsoleWriteInfo($"Saved services catalog to file: {Strings.ServicesFile}");

                        //retry operations
                        return GetServices();
                    }
                    else
                    {
                        //report error
                        ConsoleWriters.ConsoleWriteError(@"Experienced an error whilst trying to download the service information catalog from Disney+");
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                ConsoleWriters.ConsoleWriteError($"Error retrieving Disney+ services: {ex.Message}");
            }

            //restore verbosity setting to default
            ConsoleWriters.DisableAllOutput = false;

            //default
            return null;
        }
    }
    public class Application
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public class CommonHeaders
    {
        [JsonProperty("X-Application-Version")]
        public string XApplicationVersion { get; set; }

        [JsonProperty("X-BAMSDK-Client-ID")]
        public string XBamsdkClientId { get; set; }

        [JsonProperty("X-BAMSDK-Platform")]
        public string XBamsdkPlatform { get; set; }

        [JsonProperty("X-BAMSDK-Version")]
        public string XBamsdkVersion { get; set; }

        [JsonProperty("X-DSS-Edge-Accept")]
        public string XDssEdgeAccept { get; set; }
    }

    public class Logger
    {
        [JsonProperty("logLevel")]
        public string LogLevel { get; set; }
    }

    public class Services
    {
        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("adEngine")]
        public AdEngine AdEngine { get; set; }

        [JsonProperty("bamIdentity")]
        public BamIdentity BamIdentity { get; set; }

        [JsonProperty("commerce")]
        public Commerce Commerce { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("customerService")]
        public CustomerService CustomerService { get; set; }

        [JsonProperty("device")]
        public Device Device { get; set; }

        [JsonProperty("drm")]
        public Drm Drm { get; set; }

        [JsonProperty("eligibility")]
        public Eligibility Eligibility { get; set; }

        [JsonProperty("entitlement")]
        public Entitlement Entitlement { get; set; }

        [JsonProperty("externalActivation")]
        public ExternalActivation ExternalActivation { get; set; }

        [JsonProperty("externalAuthorization")]
        public ExternalAuthorization ExternalAuthorization { get; set; }

        [JsonProperty("invoice")]
        public Invoice Invoice { get; set; }

        [JsonProperty("media")]
        public Media Media { get; set; }

        [JsonProperty("orchestration")]
        public Orchestration Orchestration { get; set; }

        [JsonProperty("paywall")]
        public Paywall Paywall { get; set; }

        [JsonProperty("purchase")]
        public Purchase Purchase { get; set; }

        [JsonProperty("pushMessaging")]
        public PushMessaging PushMessaging { get; set; }

        [JsonProperty("session")]
        public Session Session { get; set; }

        [JsonProperty("socket")]
        public Socket Socket { get; set; }

        [JsonProperty("subscription")]
        public Subscription Subscription { get; set; }

        [JsonProperty("telemetry")]
        public Telemetry Telemetry { get; set; }

        [JsonProperty("token")]
        public Token Token { get; set; }
    }

    public class Account
    {
        [JsonProperty("client")]
        public AccountClient Client { get; set; }
    }

    public class AccountClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Dictionary<string, Endpoint> Endpoints { get; set; }
    }

    public class Endpoint
    {
        [JsonProperty("headers")]
        public EndpointHeaders Headers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }

        [JsonProperty("optionalHeaders", NullValueHandling = NullValueHandling.Ignore)]
        public EndpointOptionalHeaders OptionalHeaders { get; set; }
    }

    public class EndpointHeaders
    {
        [JsonProperty("Accept")]
        public string Accept { get; set; }

        [JsonProperty("Authorization")]
        public string Authorization { get; set; }

        [JsonProperty("Content-Type")]
        public string ContentType { get; set; }

        [JsonProperty("X-Bamtech-Request-Id", NullValueHandling = NullValueHandling.Ignore)]
        public string XBamtechRequestId { get; set; }

        [JsonProperty("SOAPAction", NullValueHandling = NullValueHandling.Ignore)]
        public Uri SoapAction { get; set; }

        [JsonProperty("X-DSS-Feature-Filtering", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool? XDssFeatureFiltering { get; set; }

        [JsonProperty("X-BAMSDK-Platform-Id", NullValueHandling = NullValueHandling.Ignore)]
        public string XBamsdkPlatformId { get; set; }
    }

    public class EndpointOptionalHeaders
    {
        [JsonProperty("X-BAMTech-Purchase-Platform", NullValueHandling = NullValueHandling.Ignore)]
        public string XBamTechPurchasePlatform { get; set; }

        [JsonProperty("X-BAMTech-Sales-Platform", NullValueHandling = NullValueHandling.Ignore)]
        public string XBamTechSalesPlatform { get; set; }

        [JsonProperty("X-Content-Transaction-ID", NullValueHandling = NullValueHandling.Ignore)]
        public string XContentTransactionId { get; set; }

        [JsonProperty("X-DELOREAN", NullValueHandling = NullValueHandling.Ignore)]
        public string XDelorean { get; set; }

        [JsonProperty("X-GEO-OVERRIDE", NullValueHandling = NullValueHandling.Ignore)]
        public string XGeoOverride { get; set; }
    }

    public class AdEngine
    {
        [JsonProperty("client")]
        public AdEngineClient Client { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("extras")]
        public AdEngineExtras Extras { get; set; }
    }

    public class AdEngineClient
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public PurpleEndpoints Endpoints { get; set; }
    }

    public class PurpleEndpoints
    {
        [JsonProperty("setToken")]
        public SetToken SetToken { get; set; }

        [JsonProperty("setTokenPost")]
        public SetTokenPost SetTokenPost { get; set; }

        [JsonProperty("setTokenRedirect")]
        public SetToken SetTokenRedirect { get; set; }

        [JsonProperty("setTokenRedirectPost")]
        public SetTokenPost SetTokenRedirectPost { get; set; }
    }

    public class SetToken
    {
        [JsonProperty("headers")]
        public SetTokenHeaders Headers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }
    }

    public class SetTokenHeaders
    {
        [JsonProperty("Authorization")]
        public string Authorization { get; set; }
    }

    public class SetTokenPost
    {
        [JsonProperty("headers")]
        public SetTokenPostHeaders Headers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }
    }

    public class SetTokenPostHeaders
    {
        [JsonProperty("Authorization")]
        public string Authorization { get; set; }

        [JsonProperty("Content-Type")]
        public string ContentType { get; set; }
    }

    public class AdEngineExtras
    {
        [JsonProperty("adTargeting")]
        public AdTargeting AdTargeting { get; set; }
    }

    public class AdTargeting
    {
    }

    public class BamIdentity
    {
        [JsonProperty("client")]
        public AccountClient Client { get; set; }

        [JsonProperty("extras")]
        public BamIdentityExtras Extras { get; set; }
    }

    public class BamIdentityExtras
    {
        [JsonProperty("expirationBufferSeconds")]
        public long ExpirationBufferSeconds { get; set; }
    }

    public class Commerce
    {
        [JsonProperty("client")]
        public CommerceClient Client { get; set; }

        [JsonProperty("extras")]
        public CommerceExtras Extras { get; set; }
    }

    public class CommerceClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public FluffyEndpoints Endpoints { get; set; }

        [JsonProperty("extras")]
        public PurpleExtras Extras { get; set; }
    }

    public class FluffyEndpoints
    {
        [JsonProperty("associateAuthValuesWithPaymentMethod")]
        public Endpoint AssociateAuthValuesWithPaymentMethod { get; set; }

        [JsonProperty("createComcastPaymentMethod")]
        public Endpoint CreateComcastPaymentMethod { get; set; }

        [JsonProperty("createIDealPaymentMethod")]
        public Endpoint CreateIDealPaymentMethod { get; set; }

        [JsonProperty("createKlarnaPaymentMethod")]
        public Endpoint CreateKlarnaPaymentMethod { get; set; }

        [JsonProperty("createPaymentMethod")]
        public Endpoint CreatePaymentMethod { get; set; }

        [JsonProperty("createPaymentMethodEuCentralOne")]
        public Endpoint CreatePaymentMethodEuCentralOne { get; set; }

        [JsonProperty("createPaymentMethodEuWestOne")]
        public Endpoint CreatePaymentMethodEuWestOne { get; set; }

        [JsonProperty("createPaymentMethodUsEastOne")]
        public Endpoint CreatePaymentMethodUsEastOne { get; set; }

        [JsonProperty("createPaymentMethodUsEastTwo")]
        public Endpoint CreatePaymentMethodUsEastTwo { get; set; }

        [JsonProperty("createPaymentMethodUsWestTwo")]
        public Endpoint CreatePaymentMethodUsWestTwo { get; set; }

        [JsonProperty("getComcastConsent")]
        public Endpoint GetComcastConsent { get; set; }

        [JsonProperty("getDdcJwtToken")]
        public Endpoint GetDdcJwtToken { get; set; }

        [JsonProperty("getDefaultPaymentMethod")]
        public GetDefaultPaymentMethod GetDefaultPaymentMethod { get; set; }

        [JsonProperty("getIDealPaymentMethod")]
        public Endpoint GetIDealPaymentMethod { get; set; }

        [JsonProperty("getOrderStatus")]
        public GetDefaultPaymentMethod GetOrderStatus { get; set; }

        [JsonProperty("getPayPalCheckoutDetails")]
        public Endpoint GetPayPalCheckoutDetails { get; set; }

        [JsonProperty("getPaymentCard")]
        public GetDefaultPaymentMethod GetPaymentCard { get; set; }

        [JsonProperty("getPaymentMethod")]
        public GetDefaultPaymentMethod GetPaymentMethod { get; set; }

        [JsonProperty("listAllPaymentMethods")]
        public Endpoint ListAllPaymentMethods { get; set; }

        [JsonProperty("listPaymentCards")]
        public GetDefaultPaymentMethod ListPaymentCards { get; set; }

        [JsonProperty("lookupByZipCode")]
        public LookupByZipCode LookupByZipCode { get; set; }

        [JsonProperty("planSwitch")]
        public Endpoint PlanSwitch { get; set; }

        [JsonProperty("priceOrder")]
        public Endpoint PriceOrder { get; set; }

        [JsonProperty("redeem")]
        public Endpoint Redeem { get; set; }

        [JsonProperty("restartSubscription")]
        public Endpoint RestartSubscription { get; set; }

        [JsonProperty("resumeAccount")]
        public Endpoint ResumeAccount { get; set; }

        [JsonProperty("setPayPalCheckoutDetails")]
        public Endpoint SetPayPalCheckoutDetails { get; set; }

        [JsonProperty("shareDefaultPaymentMethod")]
        public Endpoint ShareDefaultPaymentMethod { get; set; }

        [JsonProperty("submitComcastPayment")]
        public Endpoint SubmitComcastPayment { get; set; }

        [JsonProperty("submitIDealPayment")]
        public Endpoint SubmitIDealPayment { get; set; }

        [JsonProperty("submitMercadoPayment")]
        public SubmitMercadoPayment SubmitMercadoPayment { get; set; }

        [JsonProperty("submitOrderWithPaymentMethod")]
        public Endpoint SubmitOrderWithPaymentMethod { get; set; }

        [JsonProperty("updateOrder")]
        public GetDefaultPaymentMethod UpdateOrder { get; set; }

        [JsonProperty("updatePaymentCard")]
        public Endpoint UpdatePaymentCard { get; set; }

        [JsonProperty("updatePaymentMethod")]
        public Endpoint UpdatePaymentMethod { get; set; }
    }

    public class GetDefaultPaymentMethod
    {
        [JsonProperty("headers")]
        public GetDefaultPaymentMethodHeaders Headers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }
    }

    public class GetDefaultPaymentMethodHeaders
    {
        [JsonProperty("Accept")]
        public string Accept { get; set; }

        [JsonProperty("Authorization")]
        public string Authorization { get; set; }
    }

    public class LookupByZipCode
    {
        [JsonProperty("headers")]
        public LookupByZipCodeHeaders Headers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }
    }

    public class LookupByZipCodeHeaders
    {
        [JsonProperty("Accept")]
        public string Accept { get; set; }
    }

    public class SubmitMercadoPayment
    {
        [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
        public EndpointHeaders Headers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }
    }

    public class PurpleExtras
    {
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("createPaymentMethodRegionalEndpoints")]
        public CreatePaymentMethodRegionalEndpoints CreatePaymentMethodRegionalEndpoints { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("namespaceId")]
        public long NamespaceId { get; set; }
    }

    public class CreatePaymentMethodRegionalEndpoints
    {
        [JsonProperty("eu-central-1")]
        public string EuCentral1 { get; set; }

        [JsonProperty("eu-west-1")]
        public string EuWest1 { get; set; }

        [JsonProperty("us-east-1")]
        public string UsEast1 { get; set; }

        [JsonProperty("us-east-2", NullValueHandling = NullValueHandling.Ignore)]
        public string UsEast2 { get; set; }

        [JsonProperty("us-west-2")]
        public string UsWest2 { get; set; }
    }

    public class CommerceExtras
    {
        [JsonProperty("checkOrderStatusDelay")]
        public long CheckOrderStatusDelay { get; set; }

        [JsonProperty("retryPolicy")]
        public RetryPolicy RetryPolicy { get; set; }
    }

    public class RetryPolicy
    {
        [JsonProperty("retryBasePeriod")]
        public long RetryBasePeriod { get; set; }

        [JsonProperty("retryMaxAttempts")]
        public long RetryMaxAttempts { get; set; }

        [JsonProperty("retryMaxPeriod")]
        public long RetryMaxPeriod { get; set; }

        [JsonProperty("retryMultiplier")]
        public double RetryMultiplier { get; set; }
    }

    public class Content
    {
        [JsonProperty("client")]
        public ContentClient Client { get; set; }

        [JsonProperty("extras")]
        public ContentExtras Extras { get; set; }
    }

    public class ContentClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public TentacledEndpoints Endpoints { get; set; }

        [JsonProperty("extras")]
        public FluffyExtras Extras { get; set; }
    }

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

    public class ResourcesPersisted
    {
        [JsonProperty("headers")]
        public ResourcesPersistedHeaders Headers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }
    }

    public class ResourcesPersistedHeaders
    {
        [JsonProperty("Accept")]
        public string Accept { get; set; }

        [JsonProperty("Content-Type")]
        public string ContentType { get; set; }
    }

    public class FluffyExtras
    {
        [JsonProperty("urlSizeLimit")]
        public long UrlSizeLimit { get; set; }
    }

    public class ContentExtras
    {
        [JsonProperty("queryIdDefaults")]
        public QueryIdDefaults QueryIdDefaults { get; set; }
    }

    public class QueryIdDefaults
    {
        [JsonProperty("addToWatchlist")]
        public string AddToWatchlist { get; set; }

        [JsonProperty("clearWatchlist")]
        public string ClearWatchlist { get; set; }

        [JsonProperty("removeFromWatchlist")]
        public string RemoveFromWatchlist { get; set; }
    }

    public class CustomerService
    {
        [JsonProperty("client")]
        public CustomerServiceClient Client { get; set; }
    }

    public class CustomerServiceClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public StickyEndpoints Endpoints { get; set; }
    }

    public class StickyEndpoints
    {
        [JsonProperty("createSupportCode")]
        public GetDefaultPaymentMethod CreateSupportCode { get; set; }
    }

    public class Device
    {
        [JsonProperty("client")]
        public DeviceClient Client { get; set; }
    }

    public class DeviceClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public IndigoEndpoints Endpoints { get; set; }
    }

    public class IndigoEndpoints
    {
        [JsonProperty("createDeviceGrant")]
        public Endpoint CreateDeviceGrant { get; set; }
    }

    public class Drm
    {
        [JsonProperty("client")]
        public DrmClient Client { get; set; }
    }

    public class DrmClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public IndecentEndpoints Endpoints { get; set; }

        [JsonProperty("extras")]
        public TentacledExtras Extras { get; set; }
    }

    public class IndecentEndpoints
    {
        [JsonProperty("fairPlayCapability")]
        public Endpoint FairPlayCapability { get; set; }

        [JsonProperty("fairPlayCertificate")]
        public SetToken FairPlayCertificate { get; set; }

        [JsonProperty("fairPlayLicense")]
        public Endpoint FairPlayLicense { get; set; }

        [JsonProperty("fairPlayLinearLicense")]
        public Endpoint FairPlayLinearLicense { get; set; }

        [JsonProperty("nagraCertificate")]
        public SetToken NagraCertificate { get; set; }

        [JsonProperty("nagraLicense")]
        public Endpoint NagraLicense { get; set; }

        [JsonProperty("nagraLinearLicense")]
        public Endpoint NagraLinearLicense { get; set; }

        [JsonProperty("offlineFairPlayLicense")]
        public Endpoint OfflineFairPlayLicense { get; set; }

        [JsonProperty("offlineFairPlayLicenseCheck")]
        public Endpoint OfflineFairPlayLicenseCheck { get; set; }

        [JsonProperty("offlineFairPlayLicenseRelease")]
        public Endpoint OfflineFairPlayLicenseRelease { get; set; }

        [JsonProperty("offlineFairPlayLicenseRenew")]
        public Endpoint OfflineFairPlayLicenseRenew { get; set; }

        [JsonProperty("offlineWidevineLicense")]
        public SetTokenPost OfflineWidevineLicense { get; set; }

        [JsonProperty("offlineWidevineLicenseCheck")]
        public Endpoint OfflineWidevineLicenseCheck { get; set; }

        [JsonProperty("offlineWidevineLicenseRelease")]
        public SetTokenPost OfflineWidevineLicenseRelease { get; set; }

        [JsonProperty("offlineWidevineLicenseRenew")]
        public SetTokenPost OfflineWidevineLicenseRenew { get; set; }

        [JsonProperty("playReadyCapability")]
        public SubmitMercadoPayment PlayReadyCapability { get; set; }

        [JsonProperty("playReadyLicense")]
        public SubmitMercadoPayment PlayReadyLicense { get; set; }

        [JsonProperty("playReadyLinearLicense")]
        public SubmitMercadoPayment PlayReadyLinearLicense { get; set; }

        [JsonProperty("silkKey")]
        public GetDefaultPaymentMethod SilkKey { get; set; }

        [JsonProperty("widevineCapability")]
        public SetTokenPost WidevineCapability { get; set; }

        [JsonProperty("widevineCertificate")]
        public SetToken WidevineCertificate { get; set; }

        [JsonProperty("widevineLicense")]
        public SetTokenPost WidevineLicense { get; set; }

        [JsonProperty("widevineLinearLicense")]
        public SetTokenPost WidevineLinearLicense { get; set; }
    }

    public class TentacledExtras
    {
        [JsonProperty("drmLicenseEndpoints")]
        public AdTargeting DrmLicenseEndpoints { get; set; }

        [JsonProperty("licenseEndpoints")]
        public LicenseEndpoints LicenseEndpoints { get; set; }
    }

    public class LicenseEndpoints
    {
        [JsonProperty("FAIRPLAY")]
        public Fairplay Fairplay { get; set; }

        [JsonProperty("NAGRA")]
        public Fairplay Nagra { get; set; }

        [JsonProperty("PLAYREADY")]
        public Fairplay Playready { get; set; }

        [JsonProperty("WIDEVINE")]
        public Fairplay Widevine { get; set; }
    }

    public class Fairplay
    {
        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("linear")]
        public string Linear { get; set; }

        [JsonProperty("vod")]
        public string Vod { get; set; }
    }

    public class Eligibility
    {
        [JsonProperty("client")]
        public EligibilityClient Client { get; set; }
    }

    public class EligibilityClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public HilariousEndpoints Endpoints { get; set; }
    }

    public class HilariousEndpoints
    {
        [JsonProperty("getEligibilityStatus")]
        public Endpoint GetEligibilityStatus { get; set; }
    }

    public class Entitlement
    {
        [JsonProperty("client")]
        public EntitlementClient Client { get; set; }
    }

    public class EntitlementClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public AmbitiousEndpoints Endpoints { get; set; }
    }

    public class AmbitiousEndpoints
    {
        [JsonProperty("verifyMediaRights")]
        public Endpoint VerifyMediaRights { get; set; }
    }

    public class ExternalActivation
    {
        [JsonProperty("client")]
        public ExternalActivationClient Client { get; set; }
    }

    public class ExternalActivationClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public CunningEndpoints Endpoints { get; set; }
    }

    public class CunningEndpoints
    {
        [JsonProperty("activateBundle")]
        public Endpoint ActivateBundle { get; set; }

        [JsonProperty("activateToken")]
        public Endpoint ActivateToken { get; set; }

        [JsonProperty("getActivationToken")]
        public Endpoint GetActivationToken { get; set; }
    }

    public class ExternalAuthorization
    {
        [JsonProperty("client")]
        public ExternalAuthorizationClient Client { get; set; }
    }

    public class ExternalAuthorizationClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public MagentaEndpoints Endpoints { get; set; }
    }

    public class MagentaEndpoints
    {
        [JsonProperty("createLinkGrant")]
        public Endpoint CreateLinkGrant { get; set; }
    }

    public class Invoice
    {
        [JsonProperty("client")]
        public InvoiceClient Client { get; set; }
    }

    public class InvoiceClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public FriskyEndpoints Endpoints { get; set; }
    }

    public class FriskyEndpoints
    {
        [JsonProperty("getInvoice")]
        public Endpoint GetInvoice { get; set; }

        [JsonProperty("getPaginatedInvoices")]
        public Endpoint GetPaginatedInvoices { get; set; }
    }

    public class Media
    {
        [JsonProperty("client")]
        public MediaClient Client { get; set; }

        [JsonProperty("extras")]
        public MediaExtras Extras { get; set; }
    }

    public class MediaClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public MischievousEndpoints Endpoints { get; set; }
    }

    public class MischievousEndpoints
    {
        [JsonProperty("bifThumbnail")]
        public Thumbnail BifThumbnail { get; set; }

        [JsonProperty("bifThumbnails")]
        public SetToken BifThumbnails { get; set; }

        [JsonProperty("bookmarks")]
        public GetDefaultPaymentMethod Bookmarks { get; set; }

        [JsonProperty("key")]
        public GetDefaultPaymentMethod Key { get; set; }

        [JsonProperty("mediaPayload")]
        public MediaPayload MediaPayload { get; set; }

        [JsonProperty("playbackCookie")]
        public SetToken PlaybackCookie { get; set; }

        [JsonProperty("postMediaPayload")]
        public SubmitMercadoPayment PostMediaPayload { get; set; }

        [JsonProperty("spriteSheetThumbnail")]
        public Thumbnail SpriteSheetThumbnail { get; set; }

        [JsonProperty("spriteSheetThumbnails")]
        public SetToken SpriteSheetThumbnails { get; set; }
    }

    public class Thumbnail
    {
        [JsonProperty("headers")]
        public AdTargeting Headers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }
    }

    public class MediaPayload
    {
        [JsonProperty("headers")]
        public MediaPayloadHeaders Headers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }
    }

    public class MediaPayloadHeaders
    {
        [JsonProperty("Accept")]
        public string Accept { get; set; }

        [JsonProperty("Authorization")]
        public string Authorization { get; set; }

        [JsonProperty("X-DSS-Feature-Filtering")]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool XDssFeatureFiltering { get; set; }
    }

    public class MediaExtras
    {
        [JsonProperty("cdnFallback")]
        public CdnFallback CdnFallback { get; set; }

        [JsonProperty("cookieEnabled")]
        public bool CookieEnabled { get; set; }

        [JsonProperty("isUhdAllowed")]
        public bool IsUhdAllowed { get; set; }

        [JsonProperty("playbackScenarioDefault")]
        public string PlaybackScenarioDefault { get; set; }

        [JsonProperty("playbackScenarios")]
        public PlaybackScenarios PlaybackScenarios { get; set; }

        [JsonProperty("playbackSession")]
        public PlaybackSession PlaybackSession { get; set; }

        [JsonProperty("restrictedPlaybackScenario")]
        public string RestrictedPlaybackScenario { get; set; }
    }

    public class CdnFallback
    {
        [JsonProperty("fallbackLimit")]
        public long FallbackLimit { get; set; }

        [JsonProperty("isEnabled")]
        public bool IsEnabled { get; set; }
    }

    public class PlaybackScenarios
    {
        [JsonProperty("default")]
        public string Default { get; set; }

        [JsonProperty("limited")]
        public string Limited { get; set; }

        [JsonProperty("metered")]
        public string Metered { get; set; }

        [JsonProperty("unlimited")]
        public string Unlimited { get; set; }
    }

    public class PlaybackSession
    {
        [JsonProperty("streamSampleInterval")]
        public long StreamSampleInterval { get; set; }
    }

    public class Orchestration
    {
        [JsonProperty("client")]
        public OrchestrationClient Client { get; set; }
    }

    public class OrchestrationClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public BraggadociousEndpoints Endpoints { get; set; }
    }

    public class BraggadociousEndpoints
    {
        [JsonProperty("exchangeDeviceGrantForAccessToken")]
        public ExchangeDeviceGrantForAccessToken ExchangeDeviceGrantForAccessToken { get; set; }

        [JsonProperty("globalization")]
        public ExchangeDeviceGrantForAccessToken Globalization { get; set; }

        [JsonProperty("query")]
        public ExchangeDeviceGrantForAccessToken Query { get; set; }

        [JsonProperty("refreshToken")]
        public ExchangeDeviceGrantForAccessToken RefreshToken { get; set; }

        [JsonProperty("registerDevice")]
        public ExchangeDeviceGrantForAccessToken RegisterDevice { get; set; }
    }

    public class ExchangeDeviceGrantForAccessToken
    {
        [JsonProperty("headers")]
        public EndpointHeaders Headers { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }

        [JsonProperty("optionalHeaders", NullValueHandling = NullValueHandling.Ignore)]
        public ExchangeDeviceGrantForAccessTokenOptionalHeaders OptionalHeaders { get; set; }
    }

    public class ExchangeDeviceGrantForAccessTokenOptionalHeaders
    {
        [JsonProperty("X-BAMTech-Globalization-Version")]
        public string XBamTechGlobalizationVersion { get; set; }
    }

    public class Paywall
    {
        [JsonProperty("client")]
        public PaywallClient Client { get; set; }
    }

    public class PaywallClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Endpoints1 Endpoints { get; set; }
    }

    public class Endpoints1
    {
        [JsonProperty("paywall")]
        public Endpoint Paywall { get; set; }
    }

    public class Purchase
    {
        [JsonProperty("client")]
        public PurchaseClient Client { get; set; }

        [JsonProperty("extras")]
        public PurchaseExtras Extras { get; set; }
    }

    public class PurchaseClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Endpoints2 Endpoints { get; set; }
    }

    public class Endpoints2
    {
        [JsonProperty("redeemPurchases")]
        public Endpoint RedeemPurchases { get; set; }

        [JsonProperty("restorePurchases")]
        public Endpoint RestorePurchases { get; set; }
    }

    public class PurchaseExtras
    {
        [JsonProperty("retryPolicy")]
        public RetryPolicy RetryPolicy { get; set; }
    }

    public class PushMessaging
    {
        [JsonProperty("client")]
        public PushMessagingClient Client { get; set; }
    }

    public class PushMessagingClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Endpoints3 Endpoints { get; set; }
    }

    public class Endpoints3
    {
        [JsonProperty("registerChannel")]
        public RegisterChannel RegisterChannel { get; set; }
    }

    public class RegisterChannel
    {
        [JsonProperty("headers")]
        public RegisterChannelHeaders Headers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }
    }

    public class RegisterChannelHeaders
    {
        [JsonProperty("Authorization")]
        public string Authorization { get; set; }

        [JsonProperty("X-BAMTech-App-Id")]
        public string XBamTechAppId { get; set; }
    }

    public class Session
    {
        [JsonProperty("client")]
        public SessionClient Client { get; set; }
    }

    public class SessionClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Endpoints4 Endpoints { get; set; }
    }

    public class Endpoints4
    {
        [JsonProperty("getInfo")]
        public Endpoint GetInfo { get; set; }

        [JsonProperty("getLocation")]
        public Endpoint GetLocation { get; set; }
    }

    public class Socket
    {
        [JsonProperty("client")]
        public SocketClient Client { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("extras")]
        public SocketExtras Extras { get; set; }
    }

    public class SocketClient
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Dictionary<string, SubmitMercadoPayment> Endpoints { get; set; }

        [JsonProperty("extras")]
        public StickyExtras Extras { get; set; }
    }

    public class StickyExtras
    {
        [JsonProperty("connectionPairingEndpointMapping")]
        public CreatePaymentMethodRegionalEndpoints ConnectionPairingEndpointMapping { get; set; }

        [JsonProperty("pairedConnectionEndpointMapping")]
        public CreatePaymentMethodRegionalEndpoints PairedConnectionEndpointMapping { get; set; }

        [JsonProperty("protocolHeaders")]
        public ProtocolHeaders ProtocolHeaders { get; set; }

        [JsonProperty("regionalEndpointMapping")]
        public CreatePaymentMethodRegionalEndpoints RegionalEndpointMapping { get; set; }

        [JsonProperty("supportedProtocols")]
        public string[] SupportedProtocols { get; set; }
    }

    public class ProtocolHeaders
    {
        [JsonProperty("json")]
        public string Json { get; set; }

        [JsonProperty("protobuf")]
        public string Protobuf { get; set; }
    }

    public class SocketExtras
    {
        [JsonProperty("messageDeduplicationStoreSize")]
        public long MessageDeduplicationStoreSize { get; set; }

        [JsonProperty("pingPolicy")]
        public PingPolicy PingPolicy { get; set; }

        [JsonProperty("retryPolicy")]
        public RetryPolicy RetryPolicy { get; set; }

        [JsonProperty("unacknowledgedEventBuffer")]
        public UnacknowledgedEventBuffer UnacknowledgedEventBuffer { get; set; }
    }

    public class PingPolicy
    {
        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("pingInterval")]
        public long PingInterval { get; set; }

        [JsonProperty("pingMaxAttempts")]
        public long PingMaxAttempts { get; set; }

        [JsonProperty("pongTimeout")]
        public long PongTimeout { get; set; }
    }

    public class UnacknowledgedEventBuffer
    {
        [JsonProperty("maxSize")]
        public long MaxSize { get; set; }
    }

    public class Subscription
    {
        [JsonProperty("client")]
        public SubscriptionClient Client { get; set; }
    }

    public class SubscriptionClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Endpoints5 Endpoints { get; set; }
    }

    public class Endpoints5
    {
        [JsonProperty("getAccountSubscription")]
        public Endpoint GetAccountSubscription { get; set; }

        [JsonProperty("getSubscriberInfo")]
        public Endpoint GetSubscriberInfo { get; set; }

        [JsonProperty("getSubscriptions")]
        public Endpoint GetSubscriptions { get; set; }

        [JsonProperty("linkSubscriptionsFromDevice")]
        public Endpoint LinkSubscriptionsFromDevice { get; set; }
    }

    public class Telemetry
    {
        [JsonProperty("client")]
        public TelemetryClient Client { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("extras")]
        public TelemetryExtras Extras { get; set; }
    }

    public class TelemetryClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Endpoints6 Endpoints { get; set; }
    }

    public class Endpoints6
    {
        [JsonProperty("dustEvent")]
        public SetTokenPost DustEvent { get; set; }

        [JsonProperty("postEvent")]
        public SetTokenPost PostEvent { get; set; }

        [JsonProperty("validateDustEvent")]
        public SetTokenPost ValidateDustEvent { get; set; }
    }

    public class TelemetryExtras
    {
        [JsonProperty("batchLimit")]
        public long BatchLimit { get; set; }

        [JsonProperty("bufferConfigurationDefault")]
        public BufferConfigurationDefault BufferConfigurationDefault { get; set; }

        [JsonProperty("eventBufferConfiguration")]
        public BufferConfigurationDefault EventBufferConfiguration { get; set; }

        [JsonProperty("fastTrack")]
        public FastTrack FastTrack { get; set; }

        [JsonProperty("glimpseBufferConfiguration")]
        public BufferConfigurationDefault GlimpseBufferConfiguration { get; set; }

        [JsonProperty("permitAppDustEvents")]
        public bool PermitAppDustEvents { get; set; }

        [JsonProperty("prohibited")]
        public FastTrack Prohibited { get; set; }

        [JsonProperty("qosBufferConfiguration")]
        public BufferConfigurationDefault QosBufferConfiguration { get; set; }

        [JsonProperty("replyAfterFallback")]
        public long ReplyAfterFallback { get; set; }

        [JsonProperty("streamSampleBufferConfiguration")]
        public BufferConfigurationDefault StreamSampleBufferConfiguration { get; set; }
    }

    public class BufferConfigurationDefault
    {
        [JsonProperty("batchLimit")]
        public long BatchLimit { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("minimumBatchSize")]
        public long MinimumBatchSize { get; set; }

        [JsonProperty("replyAfterFallback")]
        public long ReplyAfterFallback { get; set; }
    }

    public class FastTrack
    {
        [JsonProperty("urns")]
        public string[] Urns { get; set; }
    }

    public class Token
    {
        [JsonProperty("client")]
        public TokenClient Client { get; set; }

        [JsonProperty("extras")]
        public TokenExtras Extras { get; set; }
    }

    public class TokenClient
    {
        [JsonProperty("baseUrl")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public Endpoints7 Endpoints { get; set; }

        [JsonProperty("extras")]
        public IndigoExtras Extras { get; set; }
    }

    public class Endpoints7
    {
        [JsonProperty("exchange")]
        public Endpoint Exchange { get; set; }
    }

    public class IndigoExtras
    {
        [JsonProperty("platformId")]
        public string PlatformId { get; set; }

        [JsonProperty("setCookie")]
        public bool SetCookie { get; set; }
    }

    public class TokenExtras
    {
        [JsonProperty("autoRefreshRetryPolicy")]
        public RetryPolicy AutoRefreshRetryPolicy { get; set; }

        [JsonProperty("refreshThreshold")]
        public double RefreshThreshold { get; set; }

        [JsonProperty("subjectTokenTypes")]
        public SubjectTokenTypes SubjectTokenTypes { get; set; }
    }

    public class SubjectTokenTypes
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("temporary")]
        public string Temporary { get; set; }
    }

    internal class MethodConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Method) || t == typeof(Method?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "DELETE":
                    return Method.DELETE;
                case "GET":
                    return Method.GET;
                case "PATCH":
                    return Method.PATCH;
                case "POST":
                    return Method.POST;
                case "PUT":
                    return Method.PUT;
            }
            throw new Exception("Cannot unmarshal type Method");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Method)untypedValue;
            switch (value)
            {
                case Method.DELETE:
                    serializer.Serialize(writer, "DELETE");
                    return;
                case Method.GET:
                    serializer.Serialize(writer, "GET");
                    return;
                case Method.PATCH:
                    serializer.Serialize(writer, "PATCH");
                    return;
                case Method.POST:
                    serializer.Serialize(writer, "POST");
                    return;
                case Method.PUT:
                    serializer.Serialize(writer, "PUT");
                    return;
            }
            throw new Exception("Cannot marshal type Method");
        }

        public static readonly MethodConverter Singleton = new MethodConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (bool.TryParse(value, out var b))
            {
                return b;
            }
            throw new Exception("Cannot unmarshal type bool");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (bool)untypedValue;
            var boolString = value ? "true" : "false";
            serializer.Serialize(writer, boolString);
        }
    }
}