using System.Text;

namespace WVCore
{
    public static class WVHttpUtil
    {
        public static string DefaultUserAgent => @"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:126.0) Gecko/20100101 Firefox/126.0";

        public static HttpClient Client { get; set; } = new(new HttpClientHandler
        {
            AllowAutoRedirect = true,
        });

        public static byte[] PostData(string url, Dictionary<string, string> headers, string postData)
        {
            var mediaType = postData.StartsWith("{") ? "application/json" : "application/x-www-form-urlencoded";
            var content = new StringContent(postData, Encoding.UTF8, mediaType);
            var response = Post(url, headers, content);
            var bytes = response.Content.ReadAsByteArrayAsync().Result;
            return bytes;
        }

        public static byte[] PostData(string url, Dictionary<string, string> headers, byte[] postData)
        {
            var content = new ByteArrayContent(postData);
            var response = Post(url, headers, content);
            var bytes = response.Content.ReadAsByteArrayAsync().Result;
            return bytes;
        }

        public static byte[] PostData(string url, Dictionary<string, string> headers, Dictionary<string, string> postData)
        {
            var content = new FormUrlEncodedContent(postData);
            var response = Post(url, headers, content);
            var bytes = response.Content.ReadAsByteArrayAsync().Result;
            return bytes;
        }

        public static string GetWebSource(string url, Dictionary<string, string>? headers = null)
        {
            var response = Get(url, headers);
            var bytes = response.Content.ReadAsByteArrayAsync().Result;
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] GetBinary(string url, Dictionary<string, string>? headers = null)
        {
            var response = Get(url, headers);
            var bytes = response.Content.ReadAsByteArrayAsync().Result;
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        private static HttpResponseMessage Get(string url, Dictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            if (headers == null)
                return Send(request);
            foreach (var (key, value) in headers)
                request.Headers.TryAddWithoutValidation(key, value);

            return Send(request);
        }

        private static HttpResponseMessage Post(string url, Dictionary<string, string> headers, HttpContent content)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post,
                Content = content
            };

            foreach (var (key, value) in headers)
            {
                request.Headers.TryAddWithoutValidation(key, value);
            }

            return Send(request);
        }

        private static HttpResponseMessage Send(HttpRequestMessage request)
        {
            return Client.SendAsync(request).Result;
        }
    }
}