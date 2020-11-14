using System.Collections.Generic;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace DisneyDown.Common.API.Lib
{
    public static class Constants
    {
        public const string API_KEY = @"ZGlzbmV5JmFuZHJvaWQmMS4wLjA.bkeb0m230uUhv8qrAXuNu39tbE_mD5EEhM_NAcohjyA";

        public const string CONFIG_URL =
            @"https://bam-sdk-configs.bamgrid.com/bam-sdk/v2.0/disney-svod-3d9324fc/android/v4.9.0/google/tv/prod.json";

        public const int PAGE_SIZE = 50;
        public const int PAGE_SIZE_EPISODES = 100;

        public const string CONTINUE_WATCHING_SET_ID = @"5PLvHGO3FKt1";
        public const string CONTINUE_WATCHING_SET_TYPE = @"ContinueWatchingSet";

        public static Dictionary<string, string> GENERIC_HEADERS = new Dictionary<string, string>()
        {
            {
                "User-Agent",
                "BAMSDK/v4.9.0 (disney-svod-3d9324fc 1.3.0.0; v2.0/v4.9.0; android; tv)"
            },
            {
                "x-bamsdk-client-id",
                "disney-svod-3d9324fc"
            },
            {
                "x-bamsdk-platform",
                "android-tv"
            },
            {
                "x-bamsdk-version",
                "v4.9.0"
            },
            {
                "Accept-Encoding",
                "gzip"
            }
        };
    }
}