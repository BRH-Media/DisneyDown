using System.Security.Cryptography;
using System.Text;

namespace DisneyDown.Common.Security.Hashing
{
    public static class Sha1Helper
    {
        public static string CalculateSha1Hash(string input)
        {
            //turn string into bytes
            var inputBytes = Encoding.Default.GetBytes(input);

            //calculate hash
            var hash = CalculateSha1Hash(inputBytes);

            //convert byte array to hex string
            var final = hash.ToHex();

            //null validation
            return !string.IsNullOrEmpty(final) ? final : @"";
        }

        public static byte[] CalculateSha1Hash(byte[] input)
        {
            //hash handler
            var sha1 = SHA1.Create();

            //compute the hash from bytes provided
            var hash = sha1.ComputeHash(input);

            //return result
            return hash;
        }
    }
}