using System.Security.Cryptography;
using System.Text;

namespace DisneyDown.Common.Security
{
    public static class Md5Helper
    {
        public static string CalculateMd5Hash(string input)
        {
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = CalculateMd5Hash(inputBytes);

            //convert byte array to hex string
            var final = Md5ToHex(hash);

            return !string.IsNullOrEmpty(final) ? final : @"";
        }

        public static byte[] CalculateMd5Hash(byte[] input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(input);
            return hash;
        }

        public static string Md5ToHex(byte[] hash)
        {
            var sb = new StringBuilder();
            foreach (var t in hash)
                sb.Append(t.ToString("X2"));

            return sb.ToString();
        }
    }
}