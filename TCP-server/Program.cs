using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace MyNamespace1

{
 
    
    class TCP_server
    {
        public static async Task Main()
        {
            const string ip = "127.0.0.1";
            const int port = 8080;
    
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(5);
    
                
            var buffer = new byte[4096];
            var size = 0;
    
            var data = new MemoryStream();
                
            try
            {
                var listener = await tcpSocket.AcceptAsync();
                    
                Task.Run(async () =>
                {
                    while (true)
                    {
                        //receiving messages
                        data.SetLength(0);
                        var size = await listener.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
    
                        await data.WriteAsync(buffer, 0, size);
                        var message = data.ToArray();
                        Console.WriteLine("Сообщение от клиента: " + Encoding.UTF8.GetString(message));
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


