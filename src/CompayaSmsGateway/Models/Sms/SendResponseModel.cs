using System.Collections.Generic;
using Newtonsoft.Json;

namespace CompayaSmsGateway.Models.Sms
{
    public class SendResponseModel: ResponseModel
    {
        [JsonProperty(PropertyName = "success")]
        public List<SuccessModel> Successes { get; set; }
        [JsonProperty(PropertyName = "error")]
        public List<ErrorModel> Errors { get; set; }
    }
}
