using System;

namespace DisneyDown.Common
{
    public static class Extensions
    {
        public static string ToShortForm(this TimeSpan t)
        {
            var shortForm = @"";
            var useMs = true;

            if (t.Hours > 0)
            {
                shortForm += $" {t.Hours}h";
                useMs = false;
            }
            if (t.Minutes > 0)
            {
                shortForm += $" {t.Minutes}m";
                useMs = false;
            }
            if (t.Seconds > 0)
            {
                shortForm += $" {t.Seconds}s";
                useMs = false;
            }

            if (t.Milliseconds > 0 && useMs)
                shortForm += $" {t.Milliseconds}ms";

            if (t.TotalMilliseconds == 0)
                shortForm = @"0ms";

            shortForm = shortForm.TrimStart(' ');

            return shortForm;
        }
    }
}