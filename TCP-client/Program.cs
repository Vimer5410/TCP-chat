using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Programm
{
    public class TCP_client
    {
        static async Task Main(string[] args)
        { 
            const string ip = "127.0.0.1";
            const int port = 8080;
            var Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var buffer = new byte[256];
            var size = 0;
            var data = new StringBuilder();
            
            
            try
            {
                await Socket.ConnectAsync(IPAddress.Parse(ip), port);

                Task.Run(async () =>
                {
                    while (true)
                    {
                        var size = await Socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                        data.Clear();
                        data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                        Console.WriteLine(data.ToString());
                    }
                });

                while (true)
                {
                    var message = Console.ReadLine();
                    var messageBytes = Encoding.UTF8.GetBytes(message);
                    await Socket.SendAsync(new ArraySegment<byte>(messageBytes), SocketFlags.None);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}