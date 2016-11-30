using System;

namespace CompayaSmsGateway.Models
{
    public class ResponseModel
    {
        public int HttpStatusCode { get; set; }
        public Exception Exception { get; set; }
    }
}
