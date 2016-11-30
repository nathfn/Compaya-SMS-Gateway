using Newtonsoft.Json;

namespace CompayaSmsGateway.Models.Sms
{
    public class CreditReponseModel: ResponseModel
    {
        [JsonProperty(PropertyName = "credit")]
        public string Credit { get; set; }
    }
}
