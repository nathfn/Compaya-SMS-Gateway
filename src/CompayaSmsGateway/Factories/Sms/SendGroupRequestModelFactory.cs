using CompayaSmsGateway.Models.Sms;

namespace CompayaSmsGateway.Factories.Sms
{
    internal class SendGroupRequestModelFactory
    {
        public SendGroupRequestModel BuildSendGroupSmsRequestModel(int toGroup, string message, string from, int unixTimestamp, string encoding, string dlrUrl, int flash, string reference)
        {
            var model = new SendGroupRequestModel(toGroup, message, from);
            if (unixTimestamp > 0)
                model.UnixTimestamp = unixTimestamp;
            if (flash != 0)
                model.Flash = flash;
            if (!string.IsNullOrEmpty(encoding))
                model.Encoding = encoding;
            if (!string.IsNullOrEmpty(dlrUrl))
                model.DlrUrl = dlrUrl;
            if (!string.IsNullOrEmpty(reference))
                model.Reference = reference;
            return model;
        }
    }
}
