using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyNamespace
{
    class TCP_server
    {
        static async Task Main(string[] args)
        {

            const string ip = "127.0.0.1";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(5);

            var listener = await tcpSocket.AcceptAsync();
            var buffer = new byte[256];
            var size = 0;
            var data = new StringBuilder();
            
            
            try
            {
                Task.Run(async () =>
                {
                    while (true)
                    {
                        size = await listener.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                        data.Clear();
                        data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                        Console.WriteLine(data.ToString());
                    }
                });

                while (true)
                {
                    var message = Console.ReadLine();
                    var messageBytes = Encoding.UTF8.GetBytes(message);
                    await listener.SendAsync(new ArraySegment<byte>(messageBytes), SocketFlags.None);
                }

            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

