using Newtonsoft.Json;

namespace CompayaSmsGateway.Models.Contacts
{
    public class ContactResponseModel: ResponseModel
    {
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty(PropertyName = "contactName")]
        public string ContactName { get; set; }
    }
}
