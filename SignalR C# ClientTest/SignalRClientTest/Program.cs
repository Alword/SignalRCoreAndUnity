using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnection("http://localhost:59890");
            var chat = connection.CreateHubProxy("chat");
            //Start connection

            connection.Start().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();

            chat.Invoke<string>("Send","User", "HELLO World ").ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error calling send: {0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });

            chat.On<string>("broadcastMessage", param => {
                Console.WriteLine(param);
            });
            Console.Read();
            connection.Stop();
        }
    }
}
