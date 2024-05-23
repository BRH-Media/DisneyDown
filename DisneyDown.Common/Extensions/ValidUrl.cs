using System.Text.RegularExpressions;

namespace DisneyDown.Common.Extensions
{
    public static class ValidUrl
    {
        public static bool IsValidUrl(this string url)
        {
            const string pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            var rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return rgx.IsMatch(url);
        }
    }
}