using System;
using System.Threading.Tasks;

namespace UniSdk
{
    public class UniOtpService
    {
        private UniClient _client;

        public UniOtpService(UniClient client)
        {
            _client = client;
        }

        private bool isValid(UniResponse resp)
        {
            return resp.Data != null && (bool) resp.Data.GetValue("valid");
        }

        public Task<UniResponse> SendAsync(object data)
        {
            return _client.RequestAsync("otp.send", data);
        }

        public UniResponse Send(object data)
        {
            return _client.Request("otp.send", data);
        }

        public async Task<UniResponse> VerifyAsync(object data)
        {
            var resp = await _client.RequestAsync("otp.verify", data);
            resp.Valid = isValid(resp);
            return resp;
        }

        public UniResponse Verify(object data)
        {
            var resp = _client.Request("otp.verify", data);
            resp.Valid = isValid(resp);
            return resp;
        }
    }
}
