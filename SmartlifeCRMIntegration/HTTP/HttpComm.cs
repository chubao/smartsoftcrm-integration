using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartlifeCRMIntegration.HTTP
{
    public class HttpComm
    {
        public static string POST(string url, string json) {
            string result = "";
            using (var client = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                result = client.UploadString(url, "POST", json);
            }
            return result;
        }

        public static string GET(string url, string json)
        {
            string result = "";
            using (var client = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                result = client.UploadString(url, "POST", json);
            }
            return result;
        }
    }
}
