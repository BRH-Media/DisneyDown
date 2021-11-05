using Newtonsoft.Json;
using RestSharp;
using System;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
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
}