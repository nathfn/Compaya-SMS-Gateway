using Newtonsoft.Json;

namespace CompayaSmsGateway.Models.Sms
{
    public class ErrorModel
    {
        [JsonProperty(PropertyName = "errorCode")]
        public int ErrorCode { get; set; }
        [JsonProperty(PropertyName = "errMsg")]
        public string ErrorMessage { get; set; }
        [JsonProperty(PropertyName = "to")]
        public string To { get; set; }
    }
}
