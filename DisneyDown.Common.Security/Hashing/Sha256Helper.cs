using System.Security.Cryptography;
using System.Text;

namespace DisneyDown.Common.Security.Hashing
{
    public static class Sha256Helper
    {
        public static string CalculateSha256Hash(string input)
        {
            //turn string into bytes
            var inputBytes = Encoding.Default.GetBytes(input);

            //calculate hash
            var hash = CalculateSha256Hash(inputBytes);

            //convert byte array to hex string
            var final = Sha256ToHex(hash);

            //null validation
            return !string.IsNullOrEmpty(final) ? final : @"";
        }

        public static byte[] CalculateSha256Hash(byte[] input)
        {
            //hash handler
            var sha256 = SHA256.Create();

            //compute the hash from bytes provided
            var hash = sha256.ComputeHash(input);

            //return result
            return hash;
        }

        public static string Sha256ToHex(byte[] hash)
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