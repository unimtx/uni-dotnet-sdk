using System;
using System.Threading.Tasks;

namespace UniSdk
{
    public class UniMessageService
    {
        private UniClient _client;

        public UniMessageService(UniClient client)
        {
            _client = client;
        }

        public Task<UniResponse> SendAsync(object data)
        {
            return _client.RequestAsync("sms.message.send", data);
        }

        public UniResponse Send(object data)
        {
            return _client.Request("sms.message.send", data);
        }
    }
}
