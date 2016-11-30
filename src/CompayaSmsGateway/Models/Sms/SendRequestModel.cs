using Newtonsoft.Json;

namespace CompayaSmsGateway.Models.Sms
{
    internal class SendRequestModel
    {
        public SendRequestModel(string[] to, string message, string @from)
        {
            To = to;
            Message = message;
            From = @from;
        }

        [JsonProperty(PropertyName = "to")]
        public string[] To { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }
        [JsonProperty(PropertyName = "timestamp")]
        public int? UnixTimestamp { get; set; }
        [JsonProperty(PropertyName = "encoding")]
        public string Encoding { get; set; }
        [JsonProperty(PropertyName = "dlr_url")]
        public string DlrUrl { get; set; }
        [JsonProperty(PropertyName = "flash")]
        public int? Flash { get; set; }
        [JsonProperty(PropertyName = "reference")]
        public string Reference { get; set; }
    }
}
