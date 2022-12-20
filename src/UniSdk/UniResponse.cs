using System;
using System.Linq;
using Flurl.Http;

namespace UniSdk
{
    public class UniResponseBody
    {
        public string code;
        public string message;
        public object data;
    }

    public class UniRawResponseWithReadBody
    {
        public IFlurlResponse Response { get; }
        public UniResponseBody ResponseBody { get; }

        public UniRawResponseWithReadBody(IFlurlResponse response, UniResponseBody body)
        {
            Response = response;
            ResponseBody = body;
        }
    }

    public class UniResponse
    {
        public int Status;
        public string RequestId;
        public string Code;
        public string Message;
        public object Data;
        public IFlurlResponse Raw;

        private static string _REQUEST_ID_HEADER_KEY = "X-Uni-Request-Id";

        public UniResponse(UniRawResponseWithReadBody result)
        {
            var response = result.Response;
            var body = result.ResponseBody;

            Raw = response;
            Status = response.StatusCode;
            RequestId = response.Headers.FirstOrDefault(h => h.Name == _REQUEST_ID_HEADER_KEY).Value;
            Code = body.code;
            Message = body.message;
            Data = body.data;

            if (Code != "0")
                throw new UniException(Message ?? response.ResponseMessage.ReasonPhrase, this);
        }
    }
}
