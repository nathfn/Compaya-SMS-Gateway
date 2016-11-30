using Newtonsoft.Json;

namespace CompayaSmsGateway.Models.Groups
{
    internal class GroupRequestModel
    {
        [JsonProperty(PropertyName = "groupId")]
        public int? GroupId { get; set; }
        [JsonProperty(PropertyName = "groupName")]
        public string GroupName { get; set; }
    }
}
