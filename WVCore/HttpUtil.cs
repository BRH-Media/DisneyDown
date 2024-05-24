using System.Text;

namespace WVCore
{
    internal class HttpUtil
    {
        public static HttpClient Client { get; set; } = new HttpClient(new HttpClientHandler
        {
            AllowAutoRedirect = true,
            //Proxy = null
        });

        public static byte[] PostData(string URL, Dictionary<string, string> headers, string postData)
        {
            var mediaType = postData.StartsWith("{") ? "application/json" : "application/x-www-form-urlencoded";
            var content = new StringContent(postData, Encoding.UTF8, mediaType);
            //ByteArrayContent content = new ByteArrayContent(postData);

            var response = Post(URL, headers, content);
            var bytes = response.Content.ReadAsByteArrayAsync().Result;
            return bytes;
        }

        public static byte[] PostData(string URL, Dictionary<string, string> headers, byte[] postData)
        {
            var content = new ByteArrayContent(postData);

            var response = Post(URL, headers, content);
            var bytes = response.Content.ReadAsByteArrayAsync().Result;
            return bytes;
        }

        public static byte[] PostData(string URL, Dictionary<string, string> headers, Dictionary<string, string> postData)
        {
            var content = new FormUrlEncodedContent(postData);

            var response = Post(URL, headers, content);
            var bytes = response.Content.ReadAsByteArrayAsync().Result;
            return bytes;
        }

        public static string GetWebSource(string URL, Dictionary<string, string> headers = null)
        {
            var response = Get(URL, headers);
            var bytes = response.Content.ReadAsByteArrayAsync().Result;
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] GetBinary(string URL, Dictionary<string, string> headers = null)
        {
            var response = Get(URL, headers);
            var bytes = response.Content.ReadAsByteArrayAsync().Result;
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        private static HttpResponseMessage Get(string URL, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(URL),
                Method = HttpMethod.Get
            };

            if (headers != null)
                foreach (var header in headers)
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);

            return Send(request);
        }

        private static HttpResponseMessage Post(string URL, Dictionary<string, string> headers, HttpContent content)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(URL),
                Method = HttpMethod.Post,
                Content = content
            };

            foreach (var (key, value) in headers)
                request.Headers.TryAddWithoutValidation(key, value);

            return Send(request);
        }

        private static HttpResponseMessage Send(HttpRequestMessage request)
        {
            return Client.SendAsync(request).Result;
        }
    }
}