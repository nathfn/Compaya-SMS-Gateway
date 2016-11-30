using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CompayaSmsGateway.Config;
using CompayaSmsGateway.Extensions;
using CompayaSmsGateway.Factories.Groups;
using CompayaSmsGateway.Models;
using CompayaSmsGateway.Models.Groups;
using Newtonsoft.Json;

namespace CompayaSmsGateway.Repositories
{
    public class GroupRepository: BaseRepository
    {
        private readonly GroupRequestModelFactory _groupModelFactory;

        public GroupRepository(string username, string apiKey) : base(username, apiKey)
        {
            _groupModelFactory = new GroupRequestModelFactory();
        }

        /// <summary>
        /// This creates a group for contacts.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns></returns>
        public GroupResponseModel CreateGroup(string groupName)
        {
            var responseModel = new GroupResponseModel();
            try
            {
                var json =
                  JsonConvert.SerializeObject(
                      _groupModelFactory.BuildGroupRequestModel(null, groupName),
                      new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                int httpStatusCode;
                var responseJson = ExecuteRequest(Encoding.UTF8, json, Endpoints.GroupCreate, out httpStatusCode);
                var responseObject = JsonConvert.DeserializeObject<GroupResponseModel>(responseJson);
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
        /// Gets the group or null
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns></returns>
        public GroupResponseModel GetGroup(string groupName)
        {
            return ListGroups().Groups.FirstOrDefault(group => group.GroupName == groupName);
        }

        /// <summary>
        /// Gets the group or null
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <returns></returns>
        public GroupResponseModel GetGroup(int groupId)
        {
            return ListGroups().Groups.FirstOrDefault(group => group.GroupId == groupId);
        }

        /// <summary>
        /// This updates a group.
        /// </summary>
        /// <param name="groupId">The ID of the group. You can find the ID with the listGroups API function or at CPSMS.dk.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <returns></returns>
        public BasicResponseModel UpdateGroup(int groupId, string groupName)
        {
            var responseModel = new BasicResponseModel();
            try
            {
                var json =
                  JsonConvert.SerializeObject(
                      _groupModelFactory.BuildGroupRequestModel(groupId, groupName),
                      new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                int httpStatusCode;
                var responseJson = ExecuteRequest(Encoding.UTF8, json, Endpoints.GroupUpdate, out httpStatusCode, "PUT");
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
        /// With this you can list all your groups
        /// </summary>
        /// <returns></returns>
        public GroupListResponseModel ListGroups()
        {
            var response = new GroupListResponseModel();
            try
            {
                int httpStatusCode;
                var responseJson = ExecuteEmptyRequest(Endpoints.GroupList, out httpStatusCode, "GET");

                // For some reason the endpoint does not return an array if only one group is present. Instead a single instance
                // As a result we need to try to cast it to a single instance instead of a list.
                try
                {
                    // Try it as an array
                    response.Groups.AddRange(JsonConvert.DeserializeObject<List<GroupResponseModel>>(responseJson));
                }
                catch (Exception)
                {
                    // Default to single instance
                    response.Groups.Add(JsonConvert.DeserializeObject<GroupResponseModel>(responseJson));
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

        public BasicResponseModel DeleteGroup(int groupId)
        {
            var responseModel = new BasicResponseModel();
            try
            {
                var json =
                  JsonConvert.SerializeObject(
                      _groupModelFactory.BuildGroupRequestModel(groupId, null),
                      new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                int httpStatusCode;
                var responseJson = ExecuteRequest(Encoding.UTF8, json, Endpoints.GroupDelete, out httpStatusCode, "DELETE");
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
