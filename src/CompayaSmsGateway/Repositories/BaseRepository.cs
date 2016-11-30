using System;
using System.IO;
using System.Net;
using System.Text;
using CompayaSmsGateway.Extensions;

namespace CompayaSmsGateway.Repositories
{
    public abstract class BaseRepository
    {
        protected string Username;
        protected string ApiKey;

        /// <summary>
        /// CPSMS requires Basic Authorization to access the API. The token is your CPSMS username and your API key, concatenated with a colon, encoded as base64
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="apiKey">The API key.</param>
        protected BaseRepository(string username, string apiKey)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            Username = username;
            ApiKey = apiKey;
        }

        protected string GetBase64Authentication()
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(Username + ":" + ApiKey));
        }

        protected string ExecuteEmptyRequest(string url, out int httpStatusCode, string requestMethod = "POST")
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = requestMethod;
            request.ContentType = "application/json";
            request.Headers["Authorization"] = "Basic " + GetBase64Authentication();
            using (var response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    httpStatusCode = response.GetHttpStatusCode();
                    return reader.ReadToEnd();
                }
            }
        }

        protected string ExecuteRequest(Encoding encoding, string json, string url, out int httpStatusCode, string requestMethod = "POST")
        {
            var data = encoding.GetBytes(json);
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = requestMethod;
            request.ContentType = "application/json";
            request.Headers["Authorization"] = "Basic " + GetBase64Authentication();
            request.ContentLength = data.Length;
            request.GetRequestStream().Write(data, 0, data.Length);
            using (var response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    httpStatusCode = response.GetHttpStatusCode();
                    return reader.ReadToEnd();
                }
            }

        }
    }
}
