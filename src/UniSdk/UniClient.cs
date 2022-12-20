using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using Flurl;
using Flurl.Http;

namespace UniSdk
{
    public class UniClient
    {
        private static string _NAME = "uni-dotnet-sdk";
        private static string _DEFAULT_ENDPOINT = "https://api.unimtx.com";
        private static string _DEFAULT_SIGNING_ALGORITHM = "hmac-sha256";

        private static Random _random = new Random();

        private string _accessKeyId;
        private string _accessKeySecret;
        private string _endpoint;
        private string _signingAlgorithm;
        private string _userAgent;

        public UniMessageService Messages;

        public UniClient(string accessKeyId, string accessKeySecret = null, string endpoint = null)
        {
            SetAccessKeyId(accessKeyId);
            SetAccessKeySecret(accessKeySecret);
            SetEndpoint(endpoint ?? _DEFAULT_ENDPOINT);

            _signingAlgorithm = _DEFAULT_SIGNING_ALGORITHM;
            _userAgent = _NAME + '/' + AssemblyInfo.SdkVersion;

            Messages = new UniMessageService(this);
        }

        public void SetAccessKeyId(string accessKeyId)
        {
            _accessKeyId = accessKeyId;
        }

        public void SetAccessKeySecret(string accessKeySecret)
        {
            _accessKeySecret = accessKeySecret;
        }

        public void SetEndpoint(string endpoint)
        {
            _endpoint = endpoint;
        }

        private string RandomString(int length)
        {
            const string chars = "0123456789abcdef";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string HmacSHA256(string message, string key)
        {
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacSHA256 = new HMACSHA256(keyByte))
            {
                byte[] hashMessage = hmacSHA256.ComputeHash(messageBytes);
                return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
            }
        }

        private void Sign(SortedDictionary<string, string> query)
        {
            int timestamp = (Int32)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            query.Add("algorithm", _signingAlgorithm);
            query.Add("timestamp", timestamp.ToString());
            query.Add("nonce", RandomString(16));

            string strToSign = string.Join("&", query.Select(kv => kv.Key + '=' + HttpUtility.UrlEncode(kv.Value)));

            query.Add("signature", HmacSHA256(strToSign, _accessKeySecret));
        }

        private async Task<UniRawResponseWithReadBody> SendRequest(string action, object data = null)
        {
            SortedDictionary<string, string> query = new SortedDictionary<string, string>();

            query.Add("action", action);
            query.Add("accessKeyId", _accessKeyId);

            if (_accessKeySecret != null)
                Sign(query);

            var response = await  _endpoint
                .SetQueryParams(query)
                .WithHeaders(new {
                    User_Agent = _userAgent,
                    Content_Type = "application/json;charset=utf-8",
                    Accept = "application/json"
                })
                .AllowAnyHttpStatus()
                .PostJsonAsync(data);
            var body = await response.GetJsonAsync<UniResponseBody>();

            return new UniRawResponseWithReadBody(response, body);
        }

        public async Task<UniResponse> RequestAsync(string action, object data = null)
        {
            var result = await SendRequest(action, data);
            return new UniResponse(result);
        }

        public UniResponse Request(string action, object data = null)
        {
            var result = SendRequest(action, data).Result;
            return new UniResponse(result);
        }
    }
}
