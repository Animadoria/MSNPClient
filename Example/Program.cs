using System;
using System.Threading.Tasks;
using MSNPClient;

namespace Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Messenger messenger = new Messenger();
            await messenger.Connect("username", "password");
            await Task.Delay(-1);
        }
    }
}
