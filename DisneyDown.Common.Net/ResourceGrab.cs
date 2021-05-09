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
        /// <param name="frame"></param>
        /// <returns></returns>
        public static string GrabString(string uri, ServiceFrame frame = null)
        {
            //download raw bytes
            var data = GrabBytes(uri, frame);

            //convert bytes to string then return the result
            return Encoding.Default.GetString(data);
        }

        /// <summary>
        /// Downloads a raw web resource with no transforms
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static byte[] GrabBytes(string uri, ServiceFrame frame = null)
        {
            //request handler
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = ~DecompressionMethods.None,
                AllowAutoRedirect = true,
                CookieContainer = new CookieContainer()
            };

            //automatic frame state detection
            if (frame == null)

                //reset frame
                frame = new ServiceFrame();

            //request client
            var client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromHours(2)
            };

            //new request message for this session
            var request = new HttpRequestMessage(new HttpMethod(frame.Method), uri);

            //apply request headers that emulate a browser
            request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
            request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
            request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");

            //if the referrer was set, we can assign the header a value
            if (!string.IsNullOrWhiteSpace(frame.Referrer))
                request.Headers.TryAddWithoutValidation("Referer", frame.Referrer);

            //if the authorization was set, we can assign the header a value
            if (!string.IsNullOrWhiteSpace(frame.Authorization))
                request.Headers.TryAddWithoutValidation("Authorization", frame.Authorization);

            //the response from the server is retrieved using the client
            var response = client.SendAsync(request).Result;

            //raw response body (in bytes)
            var body = response.Content.ReadAsByteArrayAsync().Result;

            //return response body
            return body;
        }
    }
}