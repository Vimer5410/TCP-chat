using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Programm
{
    public class TCP_client
    {
        static void Main(string[] args)
        { 
            const string ip = "127.0.0.1";
            const int port = 8080;
            var Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Socket.Connect(ip,port);
                while (Socket.Connected==true)
                {
                    var message = Console.ReadLine();
                    var messageBytes = Encoding.UTF8.GetBytes(message);
                    Socket.Send(messageBytes);
                }
                

            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                
            }

        }
    }
}