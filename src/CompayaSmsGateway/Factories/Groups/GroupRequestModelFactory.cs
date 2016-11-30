using CompayaSmsGateway.Models.Groups;

namespace CompayaSmsGateway.Factories.Groups
{
    internal class GroupRequestModelFactory
    {
        public GroupRequestModel BuildGroupRequestModel(int? groupId, string groupName)
        {
            return new GroupRequestModel
            {
                GroupId = groupId,
                GroupName = groupName
            };
        }
    }
}
