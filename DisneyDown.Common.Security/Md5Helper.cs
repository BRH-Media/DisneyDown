using System.Security.Cryptography;
using System.Text;

namespace DisneyDown.Common.Security
{
    public static class Md5Helper
    {
        public static string CalculateMd5Hash(string input)
        {
            //turn string into bytes
            var inputBytes = Encoding.Default.GetBytes(input);

            //calculate hash
            var hash = CalculateMd5Hash(inputBytes);

            //convert byte array to hex string
            var final = Md5ToHex(hash);

            //null validation
            return !string.IsNullOrEmpty(final) ? final : @"";
        }

        public static byte[] CalculateMd5Hash(byte[] input)
        {
            //hash handler
            var md5 = MD5.Create();

            //compute hash from bytes provided
            var hash = md5.ComputeHash(input);

            //return result
            return hash;
        }

        public static string Md5ToHex(byte[] hash)
        {
            //store strings here
            var sb = new StringBuilder();

            //each byte in the hash buffer
            foreach (var t in hash)

                //append the byte in hex form
                sb.Append(t.ToString("X2"));

            //return hex-formatted string
            return sb.ToString();
        }
    }
}