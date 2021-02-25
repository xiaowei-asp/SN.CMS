using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace SN.CMS.Common
{
    public class ApiRequestResult
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int Code { get; set; }
        public static ApiRequestResult Success(object list, string message)
        {
            return new ApiRequestResult()
            {
                IsError = false,
                Data = list,
                Code = (int)HttpStatusCode.OK,
                Message = message
            };
        }

        public static ApiRequestResult Success(string message)
        {
            return new ApiRequestResult()
            {
                IsError = false,
                Code = (int)HttpStatusCode.OK,
                Message = message
            };
        }

        public static ApiRequestResult Error(string message, HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            return new ApiRequestResult()
            {
                IsError = true,
                Data = null,
                Message = message,
                Code = (int)code
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
