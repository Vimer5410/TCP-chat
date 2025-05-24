using System.ComponentModel;
using System.Net.Sockets;
using System.Text;

namespace SharedLibrary;

public class FileTransfer
{
    public string filepath { get; }
    public string fileName { get; }
    public long filelenght { get; }

    public FileTransfer(string filepath)
    {
        var Filepath = filepath;
        var fileinfo = new FileInfo(filepath);
        filelenght = fileinfo.Length;
    }

    public async Task SendFile(Socket socket)
    {
        using (var file=File.OpenRead(filepath))
        {
            byte[] buffer = new byte[4096];
            int sum;
            while ((sum=file.Read(buffer,0,buffer.Length))>0)
            {
                await socket.SendAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
            }
        }
    }

    public async Task ReceiveFile(Socket socket)
    {
        var buffer = new byte[4096];
        var newpath = "";
        
        using (var file = File.Create(newpath))
        {
            while (true)
            {
                var filebytes = await socket.ReceiveAsync(buffer, SocketFlags.None);
                if (filebytes == 0) break; 
                await file.WriteAsync(buffer,0,filebytes);
            }
        }
    }
}