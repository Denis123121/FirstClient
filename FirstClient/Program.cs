using System.Net.Sockets;
using System.Net;
using System.Text;

void Log(string msg)
{
    Console.WriteLine($"Log: {DateTime.Now}  {msg}");
}

Socket handler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("192.168.8.162"), 36546);

handler.Connect(ipEndPoint);

Log($"Connected to server {handler.RemoteEndPoint}");

Console.WriteLine("Введите сообщение");
string messageToServer = Console.ReadLine();

byte[] outputBytes = Encoding.Unicode.GetBytes(messageToServer);
handler.Send(outputBytes);

Log($"Message to server sent: {messageToServer}");
StringBuilder messageBuilder = new StringBuilder();

do
{
    byte[] inputBytes = new byte[1024];
    int countBytes = handler.Receive(inputBytes);
    messageBuilder.Append(Encoding.Unicode.GetString(inputBytes, 0, countBytes));
} while (handler.Available > 0);

string messageFromServer = messageBuilder.ToString();

Log($"Message from server received: {messageFromServer}");

handler.Shutdown(SocketShutdown.Both);
handler.Close();

Log($"Client closed");

