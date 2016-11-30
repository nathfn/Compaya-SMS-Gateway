using Newtonsoft.Json;

namespace CompayaSmsGateway.Models.Groups
{
    public class GroupResponseModel: ResponseModel
    {
        [JsonProperty(PropertyName = "groupId")]
        public int GroupId { get; set; }
        [JsonProperty(PropertyName = "groupName")]
        public string GroupName { get; set; }
    }
}
