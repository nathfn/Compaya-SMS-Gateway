using Newtonsoft.Json;

namespace CompayaSmsGateway.Models.Sms
{
    internal class DeleteRequestModel
    {
        [JsonProperty(PropertyName = "reference")]
        public string Reference { get; set; }
    }
}

