using Newtonsoft.Json;

namespace CompayaSmsGateway.Models.Sms
{
    internal class SendGroupRequestModel
    {
        public SendGroupRequestModel(int toGroup, string message, string @from)
        {
            ToGroup = toGroup;
            Message = message;
            From = @from;
        }

        [JsonProperty(PropertyName = "to_group")]
        public int ToGroup { get; set; }
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
