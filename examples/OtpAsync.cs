using System;
using System.Threading.Tasks;
using UniSdk;

namespace UniSdk.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new UniClient("your access key id", "your access key secret");

            // send a verification code to a recipient
            try
            {
                var resp = await client.Otp.SendAsync(new {
                    to = "your phone number" // in E.164 format
                });
                Console.WriteLine(resp.Data);
            }
            catch (UniException ex)
            {
                Console.WriteLine(ex);
            }

            // verify a verification code
            try
            {
                var resp = await client.Otp.VerifyAsync(new {
                    to = "your phone number", // in E.164 format
                    code = "the code you received"
                });
                Console.WriteLine(resp.Valid);
            }
            catch (UniException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
