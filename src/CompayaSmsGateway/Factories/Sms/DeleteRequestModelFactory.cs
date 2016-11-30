using CompayaSmsGateway.Models.Sms;

namespace CompayaSmsGateway.Factories.Sms
{
    internal class DeleteRequestModelFactory
    {
        public DeleteRequestModel BuildDeleteRequestModel(string reference)
        {
            return new DeleteRequestModel {Reference = reference};
        }
    }
}
