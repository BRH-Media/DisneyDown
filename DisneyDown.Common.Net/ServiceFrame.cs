using System.Collections.Generic;
using System.Net;

// ReSharper disable EmptyConstructor
// ReSharper disable UnusedMember.Global

namespace DisneyDown.Common.Net
{
    public class ServiceFrame
    {
        /// <summary>
        /// User agent for requests
        /// </summary>
        public string UserAgent { get; set; } = @"Mozilla / 5.0(Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";

        /// <summary>
        /// Cookies to send
        /// </summary>
        public List<Cookie> Cookies { get; set; } = new List<Cookie>();

        /// <summary>
        /// Value for 'Authorization' header
        /// </summary>
        public string Authorization { get; set; } = @"";

        /// <summary>
        /// HTTP Referrer
        /// </summary>
        public string Referrer { get; set; } = @"";

        /// <summary>
        /// HTTP Method
        /// </summary>
        public string Method { get; set; } = @"GET";

        /// <summary>
        /// HTTP Parameters
        /// </summary>
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();

        public ServiceFrame()
        {
            //blank initialiser
        }
    }
}