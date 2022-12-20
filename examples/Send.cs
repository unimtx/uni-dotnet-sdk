using System;
using UniSdk;

namespace UniSdk.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new UniClient("your access key id", "your access key secret");

            try
            {
                var resp = client.Messages.Send(new {
                    to = "your phone number",
                    signature = "your sender name",
                    content = "Your verification code is 2048."
                });
                Console.WriteLine(resp.Data);
            } catch (UniException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
