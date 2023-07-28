using System;
using UniSdk;

namespace UniSdk.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new UniClient("your access key id", "your access key secret");

            // send a verification code to a recipient
            try
            {
                var resp = client.Otp.Send(new {
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
                var resp = client.Otp.Verify(new {
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
