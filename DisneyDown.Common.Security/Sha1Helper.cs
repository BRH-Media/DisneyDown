using System.Security.Cryptography;
using System.Text;

namespace DisneyDown.Common.Security
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
            var final = Sha1ToHex(hash);

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

        public static string Sha1ToHex(byte[] hash)
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