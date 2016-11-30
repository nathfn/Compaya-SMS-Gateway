using System;
using System.Net;
using System.Text;
using CompayaSmsGateway.Config;
using CompayaSmsGateway.Extensions;
using CompayaSmsGateway.Factories.Sms;
using CompayaSmsGateway.Models;
using CompayaSmsGateway.Models.Sms;
using Newtonsoft.Json;

namespace CompayaSmsGateway.Repositories
{
    public class SmsRepository : BaseRepository
    {
        private readonly SendRequestModelFactory _sendSmsRequestModelFactory;
        private readonly SendGroupRequestModelFactory _groupSmsRequestModelFactory;
        private readonly DeleteRequestModelFactory _deleteRequestModelFactory;

        public SmsRepository(string username, string apiKey) : base(username, apiKey)
        {
            _sendSmsRequestModelFactory = new SendRequestModelFactory();
            _groupSmsRequestModelFactory = new SendGroupRequestModelFactory();
            _deleteRequestModelFactory = new DeleteRequestModelFactory();
        }

        /// <summary>
        /// This lets you send an SMS to one or multiple recipients.
        /// </summary>
        /// <param name="to">The recipients of the message. The number starting with country code.</param>
        /// <param name="message">The body text of the SMS message. Specifies the message to be sent. All characters allowed by the SMS protocol are accepted. If the message contains any illegal characters, they are automatically removed, and the message shortened. The maximum message length is 1530 characters, which is the length of 10 SMS'es joined together.</param>
        /// <param name="from">Set the number that the receiver will see as the sender of the SMS. It can be either numeric with a limit of 20 chars or alphanumeric with a limit of 11 chars. You can define a default from INDSTILLINGER -> GENERELT -> Standard afsendernavn.</param>
        /// <param name="unixTimestamp">If specified, the message will be sent at this time.</param>
        /// <param name="encoding">Default is UTF-8. Alternative ISO-8859-1.</param>
        /// <param name="dlrUrl">If specified, delivery reports will be POSTed to this address. If for example the dlr_url is http://google.com/ then CPSMS.dk will append ?status=x&amp;receiver=xx Example: http://google.com/?status=x&amp;receiver=xx If needed you can add your own parameters at the end of your dlr_url. See status parameters table.</param>
        /// <param name="flash">Default is 0. Specifies if the SMS should be sent as a flash SMS.</param>
        /// <param name="reference">An optional reference of your choice.</param>
        public SendResponseModel SendSms(string[] to, string message, string from, int unixTimestamp = 0, Encoding encoding = null, string dlrUrl = "",
            int flash = 0, string reference = "")
        {
            encoding = GetEncodingOrDefault(encoding);
            var json =
                   JsonConvert.SerializeObject(
                       _sendSmsRequestModelFactory.BuildSendSmsRequestModel(to, message, from, unixTimestamp,
                           Equals(encoding, Encoding.UTF8) ? "" : encoding.WebName.ToUpper(), dlrUrl, flash, reference),
                       new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return SendSms(json, encoding, Endpoints.SendSmsUrl);
        }

        /// <summary>
        /// This lets you send a SMS to recipients you have created as contacts in a group.
        /// </summary>
        /// <param name="toGroup">To group.</param>
        /// <param name="message">The body text of the SMS message. Specifies the message to be sent. All characters allowed by the SMS protocol are accepted. If the message contains any illegal characters, they are automatically removed, and the message shortened. The maximum message length is 1530 characters, which is the length of 10 SMS'es joined together.</param>
        /// <param name="from">Set the number that the receiver will see as the sender of the SMS. It can be either numeric with a limit of 20 chars or alphanumeric with a limit of 11 chars. You can define a default from INDSTILLINGER -> GENERELT -> Standard afsendernavn.</param>
        /// <param name="unixTimestamp">If specified, the message will be sent at this time.</param>
        /// <param name="encoding">Default is UTF-8. Alternative ISO-8859-1.</param>
        /// <param name="dlrUrl">If specified, delivery reports will be POSTed to this address. If for example the dlr_url is http://google.com/ then CPSMS.dk will append ?status=x&amp;receiver=xx Example: http://google.com/?status=x&amp;receiver=xx If needed you can add your own parameters at the end of your dlr_url. See status parameters table.</param>
        /// <param name="flash">Default is 0. Specifies if the SMS should be sent as a flash SMS.</param>
        /// <param name="reference">An optional reference of your choice.</param>
        /// <returns></returns>
        public SendResponseModel SendSmsToGroup(int toGroup, string message, string from, int unixTimestamp = 0,
            Encoding encoding = null, string dlrUrl = "",
            int flash = 0, string reference = "")
        {
            encoding = GetEncodingOrDefault(encoding);
            var json =
                   JsonConvert.SerializeObject(
                       _groupSmsRequestModelFactory.BuildSendGroupSmsRequestModel(toGroup, message, from, unixTimestamp,
                           Equals(encoding, Encoding.UTF8) ? "" : encoding.WebName.ToUpper(), dlrUrl, flash, reference),
                       new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            return SendSms(json, encoding, Endpoints.SendSmsUrl);
        }

        /// <summary>
        /// This lets you see how many credits you have left on your account.
        /// </summary>
        /// <returns></returns>
        public CreditReponseModel GetSmsCredit()
        {
            var responseModel = new CreditReponseModel();
            try
            {
                int httpStatusCode;
                var responseJson = ExecuteEmptyRequest(Endpoints.SmsCreditValueUrl, out httpStatusCode);
                var responseObject = JsonConvert.DeserializeObject<CreditReponseModel>(responseJson);
                responseObject.HttpStatusCode = httpStatusCode;
                return responseObject;
            }
            catch (WebException we)
            {
                responseModel.Exception = we;
                responseModel.HttpStatusCode = ((HttpWebResponse)we.Response).GetHttpStatusCode();
            }
            catch (Exception e)
            {
                responseModel.Exception = e;
            }
            return responseModel;
        }

        /// <summary>
        /// With this you can delete an SMS that has been set with a timestamp to send at some point in the future. The timestamp has to be greater than 10 minutes from delete time. You must have set a reference on the SMS to be able to delete it. Every SMS with the specified reference that meets the criteria will bee deleted</summary>
        /// <param name="reference">The identifier set by you, when you posted the SMS.</param>
        /// <returns></returns>
        public BasicResponseModel DeleteSms(string reference)
        {
            var responseModel = new BasicResponseModel();
            try
            {
                var json =
                  JsonConvert.SerializeObject(
                      _deleteRequestModelFactory.BuildDeleteRequestModel(reference),
                      new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                int httpStatusCode;
                var responseJson = ExecuteRequest(Encoding.UTF8, json, Endpoints.SmsDeleteUrl, out httpStatusCode, "DELETE");
                var responseObject = JsonConvert.DeserializeObject<BasicResponseModel>(responseJson);
                responseObject.HttpStatusCode = httpStatusCode;
                return responseObject;
            }
            catch (WebException we)
            {
                responseModel.Exception = we;
                responseModel.HttpStatusCode = ((HttpWebResponse)we.Response).GetHttpStatusCode();
            }
            catch (Exception e)
            {
                responseModel.Exception = e;
            }
            return responseModel;
        }

        private static Encoding GetEncodingOrDefault(Encoding encoding)
        {
            return encoding ?? Encoding.UTF8;
        }

        private SendResponseModel SendSms(string json, Encoding encoding, string url)
        {
            var responseModel = new SendResponseModel();
            try
            {
                int httpStatusCode;
                var responseJson = ExecuteRequest(encoding, json, url, out httpStatusCode);
                var responseObject = JsonConvert.DeserializeObject<SendResponseModel>(responseJson);
                responseObject.HttpStatusCode = httpStatusCode;
                return responseObject;
            }
            catch (WebException we)
            {
                responseModel.Exception = we;
                responseModel.HttpStatusCode = ((HttpWebResponse)we.Response).GetHttpStatusCode();
            }
            catch (Exception e)
            {
                responseModel.Exception = e;
            }
            return responseModel;
        }
    }
}
