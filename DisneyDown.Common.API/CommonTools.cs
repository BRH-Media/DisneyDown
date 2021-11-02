using DisneyDown.Common.API.Structures.RequestPayloads;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;

// ReSharper disable InvertIf

namespace DisneyDown.Common.API
{
    public static class CommonTools
    {
        public static string GetEndpoint(this string url)
            => GetEndpoint(new Uri(url));

        public static string GetEndpoint(this Uri url)
            => HttpUtility.UrlDecode(url.PathAndQuery);

        public static string ConvertToQueryString(this TokenExchangeRequestPayload payload)
        {
            var properties = from p in payload.GetType().GetProperties()
                             where p.GetValue(payload, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(payload, null).ToString());
            var queryString = string.Join("&", properties.ToArray());

            //return result
            return queryString;
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                var type = e.GetType();
                var values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val) ?? string.Empty);

                        if (memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return string.Empty;
        }
    }
}