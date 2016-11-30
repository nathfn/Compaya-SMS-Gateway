using System.Collections.Generic;

namespace CompayaSmsGateway.Models.Log
{
    public class LogListResponseModel: ResponseModel
    {
        public LogListResponseModel()
        {
            Log = new List<LogResponseModel>();
        }

        public List<LogResponseModel> Log { get; set; }
    }
}
