using System.Collections.Generic;

namespace CompayaSmsGateway.Models.Contacts
{
    public class ContactListResponseModel: ResponseModel
    {
        public ContactListResponseModel()
        {
            Contacts = new List<ContactResponseModel>();
        }

        public List<ContactResponseModel> Contacts { get; set; }
    }
}
