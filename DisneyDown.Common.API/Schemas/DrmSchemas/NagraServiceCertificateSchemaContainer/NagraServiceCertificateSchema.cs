using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.DrmSchemas.NagraServiceCertificateSchemaContainer
{
    public class NagraServiceCertificateSchema
    {
        public static NagraServiceCertificateSchema FromJson(string json)
            => JsonConvert.DeserializeObject<NagraServiceCertificateSchema>(json, ApiJsonConverter.Settings);

        [JsonProperty("SIGNED_DATA", NullValueHandling = NullValueHandling.Ignore)]
        public NagraServiceCertificateSchemaSignedData SignedData { get; set; }

        [JsonProperty("SIGNATURE", NullValueHandling = NullValueHandling.Ignore)]
        public string Signature { get; set; }
    }

    public class NagraServiceCertificateSchemaSignedData
    {
        [JsonProperty("PUBLIC_DATA", NullValueHandling = NullValueHandling.Ignore)]
        public PurplePublicData PublicData { get; set; }

        [JsonProperty("PROTECTED_DATA", NullValueHandling = NullValueHandling.Ignore)]
        public ProtectedData ProtectedData { get; set; }
    }

    public class ProtectedData
    {
        [JsonProperty("encryptedData", NullValueHandling = NullValueHandling.Ignore)]
        public string EncryptedData { get; set; }

        [JsonProperty("encryptedKey", NullValueHandling = NullValueHandling.Ignore)]
        public string EncryptedKey { get; set; }
    }

    public class PurplePublicData
    {
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        [JsonProperty("messageVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string MessageVersion { get; set; }

        [JsonProperty("creationDate", NullValueHandling = NullValueHandling.Ignore)]
        public long? CreationDate { get; set; }

        [JsonProperty("CASID", NullValueHandling = NullValueHandling.Ignore)]
        public long? Casid { get; set; }

        [JsonProperty("hlsKeyFormat", NullValueHandling = NullValueHandling.Ignore)]
        public string HlsKeyFormat { get; set; }

        [JsonProperty("dashDrmSystemId", NullValueHandling = NullValueHandling.Ignore)]
        public string DashDrmSystemId { get; set; }

        [JsonProperty("operatorProvisioningInformation", NullValueHandling = NullValueHandling.Ignore)]
        public OperatorProvisioningInformation OperatorProvisioningInformation { get; set; }

        [JsonProperty("defaultUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri DefaultUrl { get; set; }

        [JsonProperty("operatorCACertificate", NullValueHandling = NullValueHandling.Ignore)]
        public OperatorCertificate OperatorCaCertificate { get; set; }

        [JsonProperty("operatorMessagingCertificate", NullValueHandling = NullValueHandling.Ignore)]
        public OperatorCertificate OperatorMessagingCertificate { get; set; }

        [JsonProperty("operatorDeviceCACertificate", NullValueHandling = NullValueHandling.Ignore)]
        public OperatorCertificate OperatorDeviceCaCertificate { get; set; }
    }

    public class OperatorCertificate
    {
        [JsonProperty("SIGNED_DATA", NullValueHandling = NullValueHandling.Ignore)]
        public OperatorCaCertificateSignedData SignedData { get; set; }

        [JsonProperty("SIGNATURE", NullValueHandling = NullValueHandling.Ignore)]
        public Signature Signature { get; set; }
    }

    public class Signature
    {
        [JsonProperty("signingCredentialsId", NullValueHandling = NullValueHandling.Ignore)]
        public string SigningCredentialsId { get; set; }

        [JsonProperty("signatureData", NullValueHandling = NullValueHandling.Ignore)]
        public string SignatureData { get; set; }
    }

    public class OperatorCaCertificateSignedData
    {
        [JsonProperty("PUBLIC_DATA", NullValueHandling = NullValueHandling.Ignore)]
        public FluffyPublicData PublicData { get; set; }
    }

    public class FluffyPublicData
    {
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        [JsonProperty("credentialsId", NullValueHandling = NullValueHandling.Ignore)]
        public string CredentialsId { get; set; }

        [JsonProperty("creationDate", NullValueHandling = NullValueHandling.Ignore)]
        public long? CreationDate { get; set; }

        [JsonProperty("publicKey", NullValueHandling = NullValueHandling.Ignore)]
        public PublicKey PublicKey { get; set; }

        [JsonProperty("entity", NullValueHandling = NullValueHandling.Ignore)]
        public Entity Entity { get; set; }
    }

    public class Entity
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("keyType", NullValueHandling = NullValueHandling.Ignore)]
        public string KeyType { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("operatorId", NullValueHandling = NullValueHandling.Ignore)]
        public long? OperatorId { get; set; }
    }

    public class PublicKey
    {
        [JsonProperty("modulus", NullValueHandling = NullValueHandling.Ignore)]
        public string Modulus { get; set; }

        [JsonProperty("exponent", NullValueHandling = NullValueHandling.Ignore)]
        public long? Exponent { get; set; }
    }

    public class OperatorProvisioningInformation
    {
        [JsonProperty("activeGeneration", NullValueHandling = NullValueHandling.Ignore)]
        public long? ActiveGeneration { get; set; }
    }
}