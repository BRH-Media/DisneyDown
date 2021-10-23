using System;

namespace DisneyDown.Common.API
{
    public static class CommonTools
    {
        public static string GetEndpoint(this string url)
            => GetEndpoint(new Uri(url));

        public static string GetEndpoint(this Uri url)
            => url.PathAndQuery;
    }
}