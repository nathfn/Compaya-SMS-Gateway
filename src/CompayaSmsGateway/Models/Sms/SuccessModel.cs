using Newtonsoft.Json;

namespace CompayaSmsGateway.Models.Sms
{
    public class SuccessModel
    {
        [JsonProperty(PropertyName = "to")]
        public string To { get; set; }
        [JsonProperty(PropertyName = "cost")]
        public int Cost { get; set; }
    }
}
