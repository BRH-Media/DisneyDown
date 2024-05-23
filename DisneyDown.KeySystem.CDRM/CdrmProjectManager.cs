using DisneyDown.KeySystem.CDRM.Schemas;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;

namespace DisneyDown.KeySystem.CDRM
{
    public class CdrmProjectManager
    {
        public static ResponsePayload RequestMediaViaPssh(byte[] pssh, string disneyToken = @"")
        {
            try
            {
                if (pssh?.Length > 0)
                {
                    //file-based
                    if (string.IsNullOrWhiteSpace(disneyToken))
                    {
                        if (File.Exists(@"cdrmToken.txt"))
                        {
                            disneyToken = File.ReadAllText(@"cdrmToken.txt");
                        }
                        else
                        {
                            return null;
                        }
                    }

                    //setup request payload to send
                    var psshSend = Convert.ToBase64String(pssh);
                    var baseHeaders = $@"Authorization: Bearer {disneyToken}\nAccept-Language: en-US,en;q=0.5\nAccept-Encoding: gzip, deflate, br, zstd\nReferer: https://www.disneyplus.com/\nContent-Type: application/octet-stream\nX-Application-Version: 1.1.2\nX-BAMSDK-Client-ID: disney-svod-3d9324fc\nX-BAMSDK-Platform: javascript/windows/firefox\nX-DSS-Edge-Accept: vnd.dss.edge+json; version=2\nX-Request-ID: \nOrigin: https://www.disneyplus.com\nConnection: keep-alive\nSec-Fetch-Dest: empty\nSec-Fetch-Mode: cors\nSec-Fetch-Site: cross-site\nTE: trailers";
                    var requestPayload = JsonConvert.SerializeObject(new RequestPayload
                    {
                        PsshB64 = psshSend,
                        DelimitedPostHeaders = baseHeaders
                    });

                    //setup client
                    var client = new RestClient(Endpoints.BaseUrl);
                    var request = new RestRequest(Endpoints.Resource)
                    {
                        Method = Method.POST
                    };
                    request.AddJsonBody(requestPayload);

                    var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
                    var responseEncoded = response?.Content;

                    if (!string.IsNullOrWhiteSpace(responseEncoded))
                    {
                        return JsonConvert.DeserializeObject<ResponsePayload>(responseEncoded, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    }
                }
            }
            catch
            {
                //nothing
            }

            //default
            return null;
        }
    }
}