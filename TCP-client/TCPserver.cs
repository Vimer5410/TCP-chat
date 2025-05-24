using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MyNamespace
{
    public class TCP_client
    {
        private static RSA rsa;
        
        
        public static async Task Main()
        { 
            const string ip = "127.0.0.1";
            const int port = 8080;
            var Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var buffer = new byte[4096];
            var size = 0;
            var data = new MemoryStream();
            
            
            try
            {
                await Socket.ConnectAsync(IPAddress.Parse(ip), port);
                
                Task.Run(async () =>
                {
                    while (true)
                    {
                        //receiving messages
                        data.SetLength(0);
                        var size = await Socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                        
                        
                        await data.WriteAsync(buffer,0,size);
                        var message = data.ToArray();
                        Console.WriteLine("Сообщение от сервера: {0}, Размер в байтах: {1}", Encoding.UTF8.GetString(message), message.Length);
                        
                    }
                });

                while (true)
                {
                    //sending messages
                    
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

