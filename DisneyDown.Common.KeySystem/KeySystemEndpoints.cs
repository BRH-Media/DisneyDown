namespace DisneyDown.Common.KeySystem
{
    public class KeySystemEndpoints
    {
        public string ReportEndpoint { get; set; } = @"report/key.php";
        public string UserQueryEndpoint { get; set; } = @"query/findUserKey.php";
        public string UserQueryListEndpoint { get; set; } = @"query/myKeys.php";
        public string UserLoginEndpoint { get; set; } = @"auth/loginTest.php";
        public string SystemQueryListEndpoint { get; set; } = @"query/allKeys.php";
    }
}