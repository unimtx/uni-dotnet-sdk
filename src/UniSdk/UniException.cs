using System;
using Flurl.Http;

namespace UniSdk
{
    public class UniException : Exception
    {
        public UniResponse Response { get; }
        public string RequestId { get; }
        public string ErrorCode { get; }
        public string ErrorMessage { get; }

        public UniException(string message) : base(message) {}

        public UniException(string message, UniResponse response = null) : this(BuildMessage(message, response))
        {
            Response = response;
            RequestId = response?.RequestId;
            ErrorCode = response?.Code ?? "-1";
            ErrorMessage = response?.Message ?? message;
        }

        private static string BuildMessage(string message, UniResponse response)
        {
            string msg = "[" + (response?.Code ?? "-1") + "] " + (message ?? "UnknownError");

            if (response?.RequestId != null)
                msg += ", RequestId: " + response.RequestId;

            return msg;
        }
    }
}
