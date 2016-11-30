using System;
using System.Collections.Generic;
using System.Net;
using CompayaSmsGateway.Config;
using CompayaSmsGateway.Extensions;
using CompayaSmsGateway.Models.Log;
using Newtonsoft.Json;

namespace CompayaSmsGateway.Repositories
{
    public class LogRepository: BaseRepository
    {
        public LogRepository(string username, string apiKey) : base(username, apiKey)
        {
        }

        public LogListResponseModel ListLog(string to = "", int fromDate = 0, int toDate = 0)
        {
            var response = new LogListResponseModel();
            try
            {
                var url = Endpoints.LogList;
                if (fromDate != 0 && toDate != 0 && !string.IsNullOrEmpty(to))
                    url = url + "/" + fromDate + "/" + toDate + "/" + to;
                else if (fromDate != 0 && toDate != 0)
                    url = url + "/" + fromDate + "/" + toDate;
                else if (!string.IsNullOrEmpty(to))
                    url += "/" + to;

                int httpStatusCode;
                var responseJson = ExecuteEmptyRequest(url, out httpStatusCode, "GET");
                // For some reason the endpoint does not return an array if only one group is present. Instead a single instance
                // As a result we need to try to cast it to a single instance instead of a list.
                try
                {
                    // Try it as an array
                    response.Log.AddRange(JsonConvert.DeserializeObject<List<LogResponseModel>>(responseJson));
                }
                catch (Exception)
                {
                    // Default to single instance
                    response.Log.Add(JsonConvert.DeserializeObject<LogResponseModel>(responseJson));
                }
                response.HttpStatusCode = httpStatusCode;
                return response;
            }
            catch (WebException we)
            {
                response.Exception = we;
                response.HttpStatusCode = ((HttpWebResponse)we.Response).GetHttpStatusCode();
            }
            catch (Exception e)
            {
                response.Exception = e;
            }
            return response;
        }
    }
}
