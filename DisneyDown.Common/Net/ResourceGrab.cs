using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace DisneyDown.Common.Net
{
    /// <summary>
    /// Web client handler; processes web resources for download
    /// </summary>
    public static class ResourceGrab
    {
        /// <summary>
        /// Downloads a web resource then transforms it into an ASCII/UTF-8 string
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="referrer"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string GrabString(string uri, string referrer = @"", string method = @"GET")
        {
            //download raw bytes
            var data = GrabBytes(uri, referrer, method);

            //convert bytes to string then return the result
            return Encoding.Default.GetString(data);
        }

        /// <summary>
        /// Downloads a raw web resource with no transforms
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="referrer"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static byte[] GrabBytes(string uri, string referrer = @"", string method = @"GET")
        {
            //request handler
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = ~DecompressionMethods.None,
                AllowAutoRedirect = true,
                CookieContainer = new CookieContainer()
            };

            //request client
            var client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(3)
            };

            //new request message for this session
            var request = new HttpRequestMessage(new HttpMethod(method), uri);

            //apply request headers that emulate a browser
            request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
            request.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
            request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
            request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
            request.Headers.TryAddWithoutValidation("Cookie", "disableLogout=0");

            //if the referrer was set, we can assign the header a value
            if (!string.IsNullOrEmpty(referrer))
                request.Headers.TryAddWithoutValidation("Referer", referrer);

            //the response from the server is retrieved using the client
            var response = client.SendAsync(request).Result;

            //raw response body (in bytes)
            var body = response.Content.ReadAsByteArrayAsync().Result;

            //return response body
            return body;
        }
    }
}