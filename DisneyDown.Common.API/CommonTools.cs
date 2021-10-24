using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

// ReSharper disable InvertIf

namespace DisneyDown.Common.API
{
    public static class CommonTools
    {
        public static string GetEndpoint(this string url)
            => GetEndpoint(new Uri(url));

        public static string GetEndpoint(this Uri url)
            => url.PathAndQuery;

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                var type = e.GetType();
                var values = System.Enum.GetValues(type);

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