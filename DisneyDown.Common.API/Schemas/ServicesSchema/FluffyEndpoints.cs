using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
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
}