using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyNamespace
{
    class TCP_server
    {
        static void Main(string[] args)
        {
            
            const string ip = "127.0.0.1";
            const int port = 8080;
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip),port);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(5);
            var listener = tcpSocket.Accept();
            var buffer = new byte[256];
            var size = 0;
            var data = new StringBuilder();
            while (true)
            {
                size = listener.Receive(buffer);
                data.Clear();
                data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                Console.WriteLine(data.ToString());
            }
        }
    }
}

