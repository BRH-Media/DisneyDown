using System.Text;

namespace DisneyDown.Common.Security.Hashing
{
    public static class HashingTools
    {
        public static string ToHex(this byte[] bytes)
        {
            //store strings here
            var sb = new StringBuilder();

            //each byte in the hash buffer
            foreach (var t in bytes)

                //append the byte in hex form
                sb.Append(t.ToString("X2"));

            //return hex-formatted string
            return sb.ToString();
        }
    }
}