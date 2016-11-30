using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using CompayaSmsGateway.Config;
using CompayaSmsGateway.Extensions;
using CompayaSmsGateway.Factories.Contacts;
using CompayaSmsGateway.Models;
using CompayaSmsGateway.Models.Contacts;
using Newtonsoft.Json;

namespace CompayaSmsGateway.Repositories
{
    public class ContactRepository: BaseRepository
    {
        private readonly ContactRequestModelFactory _contactRequestModelFactory;

        public ContactRepository(string username, string apiKey) : base(username, apiKey)
        {
            _contactRequestModelFactory = new ContactRequestModelFactory();
        }

        /// <summary>
        /// This creates a group for contacts.
        /// </summary>
        /// <param name="groupId">Id of the group the contact is to be placed in.</param>
        /// <param name="phoneNumber">The phone number for the contact starting with country code.</param>
        /// <param name="contactName">Name/ identifier of the contact.</param>
        /// <returns></returns>
        public BasicResponseModel CreateGroup(int groupId, string phoneNumber, string contactName = null)
        {
            var responseModel = new BasicResponseModel();
            try
            {
                var json =
                  JsonConvert.SerializeObject(
                      _contactRequestModelFactory.BuildContactRequestModel(groupId, phoneNumber, contactName),
                      new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                int httpStatusCode;
                var responseJson = ExecuteRequest(Encoding.UTF8, json, Endpoints.ContactCreate, out httpStatusCode);
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

        /// <summary>
        /// This endpoint can list all contacts in a given group. You need to specify a groupId.
        /// </summary>
        /// <param name="groupId">Id of the group.</param>
        /// <returns></returns>
        public ContactListResponseModel ListContacts(int groupId)
        {
            var response = new ContactListResponseModel();
            try
            {
                int httpStatusCode;
                var responseJson = ExecuteEmptyRequest(Endpoints.ContactList + groupId, out httpStatusCode, "GET");

                // For some reason the endpoint does not return an array if only one group is present. Instead a single instance
                // As a result we need to try to cast it to a single instance instead of a list.
                try
                {
                    // Try it as an array
                    response.Contacts.AddRange(JsonConvert.DeserializeObject<List<ContactResponseModel>>(responseJson));
                }
                catch (Exception)
                {
                    // Default to single instance
                    response.Contacts.Add(JsonConvert.DeserializeObject<ContactResponseModel>(responseJson));
                }
                return response;
            }
            catch (WebException we)
            {
                response.Exception = we;
                response.HttpStatusCode = ((HttpWebResponse)we.Response).GetHttpStatusCode();
            }
            catch (Exception e)
            {
                response.Exception = e;
            }
            return response;
        }

        /// <summary>
        /// This updates a contact.
        /// </summary>
        /// <param name="groupId">The ID of the group. You can find the ID with the listGroups API function or at CPSMS.dk.</param>
        /// <param name="phoneNumber">The phone number for the contact starting with country code.</param>
        /// <param name="contactName">Name of the contact.</param>
        /// <returns></returns>
        public BasicResponseModel UpdateContact(int groupId, string phoneNumber, string contactName = null)
        {
            var responseModel = new BasicResponseModel();
            try
            {
                var json =
                  JsonConvert.SerializeObject(
                      _contactRequestModelFactory.BuildContactRequestModel(groupId, phoneNumber, contactName),
                      new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                int httpStatusCode;
                var responseJson = ExecuteRequest(Encoding.UTF8, json, Endpoints.ContactUpdate, out httpStatusCode, "PUT");
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

        /// <summary>
        /// This removes a contact from a group. If the contact is removed from all groups, it will be completely deleted.
        /// </summary>
        /// <param name="groupId">The ID of the group. You can find the ID with the listGroups API function or at CPSMS.dk.</param>
        /// <param name="phoneNumber">The contacts phone number. With country code.</param>
        /// <returns></returns>
        public BasicResponseModel DeleteContact(int groupId, string phoneNumber)
        {
            var responseModel = new BasicResponseModel();
            try
            {
                var json =
                  JsonConvert.SerializeObject(
                      _contactRequestModelFactory.BuildContactRequestModel(groupId, phoneNumber, null),
                      new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                int httpStatusCode;
                var responseJson = ExecuteRequest(Encoding.UTF8, json, Endpoints.ContactDelete, out httpStatusCode, "DELETE");
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
    }
}
