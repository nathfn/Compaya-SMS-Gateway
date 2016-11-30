using Newtonsoft.Json;

namespace CompayaSmsGateway.Models.Contacts
{
    internal class ContactRequestModel
    {
        [JsonProperty(PropertyName = "groupId")]
        public int? GroupId { get; set; }
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty(PropertyName = "contactName")]
        public string ContactName { get; set; }
    }
}
