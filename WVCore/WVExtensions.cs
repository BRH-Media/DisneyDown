using System.Text;
using Newtonsoft.Json.Linq;

namespace WVCore
{
    public static class WVExtensions
    {
        public static bool IsValidJsonBytes(this byte[] b)
        {
            try
            {
                if (b.Length > 0)
                {
                    var e = Encoding.Default.GetString(b);
                    if (!string.IsNullOrWhiteSpace(e))
                    {
                        return IsValidJson(e);
                    }
                }
            }
            catch
            {
                //nothing
            }
            return false;
        }

        public static bool IsValidJson(this string s)
        {
            try
            {
                JToken.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}