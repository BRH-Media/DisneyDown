using System;

namespace DisneyDown.Common
{
    public static class Methods
    {
        public static string GetBaseUrl(string url) => new Uri(new Uri(url), ".").OriginalString;
    }
}