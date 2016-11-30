using Newtonsoft.Json;

namespace CompayaSmsGateway.Models.Log
{
    public class LogResponseModel: ResponseModel
    {
        [JsonProperty(PropertyName = "to")]
        public string To { get; set; }
        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }
        [JsonProperty(PropertyName = "pointPrice")]
        public int PointPrice { get; set; }
        [JsonProperty(PropertyName = "userReference")]
        public string UserReference { get; set; }
        [JsonProperty(PropertyName = "dlrStatus")]
        public int DlrStatus { get; set; }
        [JsonProperty(PropertyName = "dlrStatusText")]
        public string DlrStatusText { get; set; }
        [JsonProperty(PropertyName = "timeSent")]
        public int TimeSent { get; set; }
    }
}
