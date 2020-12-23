namespace DisneyDown.Console.Dash.PrimeMPD
{
    public static class Globals
    {
        public static string PrimeCdnDomain { get; } = @"dash.row.aiv-cdn.net";

        public static bool IsValidPrimeManifestUrl(string url) => url.Contains(PrimeCdnDomain);
    }
}