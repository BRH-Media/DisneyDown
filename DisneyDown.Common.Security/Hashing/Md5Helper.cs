using System.Security.Cryptography;
using System.Text;

namespace DisneyDown.Common.Security.Hashing
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
            var final = hash.ToHex();

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
    }
}