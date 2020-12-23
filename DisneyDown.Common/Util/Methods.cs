using System;

namespace DisneyDown.Common.Util
{
    /// <summary>
    /// Generic methods that are commonly accessed
    /// </summary>
    public static class Methods
    {
        /// <summary>
        /// Base URL from string; gets the URL 'directory' of a file resource
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetBaseUrl(string url)
            => new Uri(new Uri(url), ".").OriginalString;
    }
}