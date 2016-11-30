using CompayaSmsGateway.Models.Sms;

namespace CompayaSmsGateway.Factories.Sms
{
    internal class SendRequestModelFactory
    {
        public SendRequestModel BuildSendSmsRequestModel(string[] to, string message, string from, int unixTimestamp, string encoding, string dlrUrl, int flash, string reference)
        {
            var model = new SendRequestModel(to, message, from);
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
