using System;

namespace WidevineLicensor
{
    public static class Extensions
    {
        public static bool IsBase64(this string base64String)
        {
            if (string.IsNullOrEmpty(base64String)
                || base64String.Length % 4 != 0
                || base64String.Contains(" ")
                || base64String.Contains("\t")
                || base64String.Contains("\r")
                || base64String.Contains("\n"))

                return false;

            try
            {
                var _ = Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception)
            {
                // Handle the exception
            }
            return false;
        }

        public static string ToBase64(this byte[] inputString)
            => Convert.ToBase64String(inputString);

        public static byte[] FromBase64(this string base64String)
            => Convert.FromBase64String(base64String);
    }
}