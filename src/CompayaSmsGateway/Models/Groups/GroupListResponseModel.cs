using System.Collections.Generic;

namespace CompayaSmsGateway.Models.Groups
{
    public class GroupListResponseModel: ResponseModel
    {
        public GroupListResponseModel()
        {
            Groups = new List<GroupResponseModel>();
        }

        public List<GroupResponseModel> Groups { get; set; }
    }
}
