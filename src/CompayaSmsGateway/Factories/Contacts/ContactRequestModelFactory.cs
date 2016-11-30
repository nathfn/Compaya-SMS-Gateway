using CompayaSmsGateway.Models.Contacts;

namespace CompayaSmsGateway.Factories.Contacts
{
    internal class ContactRequestModelFactory
    {
        public ContactRequestModel BuildContactRequestModel(int? groupId, string phoneNumber, string contactName)
        {
            return new ContactRequestModel { GroupId = groupId, PhoneNumber = phoneNumber, ContactName = contactName };
        }
    }
}
