namespace DisneyDown.Console.Dash.Common
{
    public static class Extensions
    {
        public static string RemoveHyphens(this string inputString)
        {
            return inputString.Replace("-", "");
        }
    }
}