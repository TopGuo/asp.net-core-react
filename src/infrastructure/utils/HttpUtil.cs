using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace infrastructure.utils
{
    public class HttpUtil
    {
        /// <summary>
        /// Get Req
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetString(string url)
        {
            HttpMessageHandler handler = new HttpClientHandler();
            using (HttpClient client = new HttpClient(handler))
            {
                return client.GetStringAsync(url).GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// Post Req
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="cert"></param>
        /// <param name="contentType">application/x-www-form-urlencoded,multipart/form-data,application/json</param>
        /// <returns></returns>
        public static string PostString(string url, string postData, string contentType = "application/x-www-form-urlencoded", X509Certificate2 cert = null,
            string headerAuthorization = "",
            string charset = "utf-8",
            string contentCharset = "utf-8")
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.UseDefaultCredentials = true;
                if (cert != null)
                {
                    handler.ClientCertificates.Add(cert);
                }
                handler.ServerCertificateCustomValidationCallback = (x, y, z, m) =>
                {
                    return true;
                };
                handler.AllowAutoRedirect = true;
                handler.UseCookies = true;
                using (HttpClient client = new HttpClient(handler))
                {
                    var h = client.DefaultRequestHeaders;
                    h.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                    StringContent content = new StringContent(postData, Encoding.GetEncoding(charset), contentType);
                    if (!string.IsNullOrEmpty(headerAuthorization))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", headerAuthorization);
                    }

                    var c = client.PostAsync(url, content);
                    c.Wait();
                    var response = c.Result;

                    var bytes = response.Content.ReadAsByteArrayAsync().Result;

                    var encoding = System.Text.Encoding.GetEncoding(contentCharset);
                    var str = encoding.GetString(bytes);
                    return str;
                }
            }
            catch (Exception ex) { throw new Exception(url + " :" + ex.Message); }
        }
    }
}