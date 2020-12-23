using System.Security.Cryptography;
using System.Text;

namespace DisneyDown.Common.Security
{
    public static class Sha256Helper
    {
        public static string CalculateSha256Hash(string input)
        {
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = CalculateSha256Hash(inputBytes);

            //convert byte array to hex string
            var final = Sha256ToHex(hash);

            return !string.IsNullOrEmpty(final) ? final : @"";
        }

        public static byte[] CalculateSha256Hash(byte[] input)
        {
            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(input);
            return hash;
        }

        public static string Sha256ToHex(byte[] hash)
        {
            var sb = new StringBuilder();
            foreach (var t in hash)
                sb.Append(t.ToString("X2"));

            return sb.ToString();
        }
    }
}