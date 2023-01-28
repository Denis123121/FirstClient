using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FirstClient.NetEngine;

public class ClientEngine
{
    private Socket _clientSocket;
    private IPEndPoint _ipEndPoint;

    public ClientEngine(string ip, int port)
    {
        _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
    }

    private void Log(string msg)
    {
        Console.WriteLine($"LOG: {DateTime.Now} --- {msg}");
    }

    public void ConnectToServer()
    {
        _clientSocket.Connect(_ipEndPoint);

        Log($"CONNECTED TO SERVER {_clientSocket.RemoteEndPoint}");
    }

    public void SendMessage(string messageToServer)
    {
        byte[] outputBytes = Encoding.Unicode.GetBytes(messageToServer);
        _clientSocket.Send(outputBytes);

        Log($"MESSAGE TO SERVER SENT: {messageToServer}");
    }

    public string ReceiveMessage()
    {
        StringBuilder messageBuilder = new StringBuilder();
        do
        {
            byte[] inputBytes = new byte[1024];
            int countBytes = _clientSocket.Receive(inputBytes);
            messageBuilder.Append(Encoding.Unicode.GetString(inputBytes, 0, countBytes));
        } while (_clientSocket.Available > 0);

        string messageFromServer = messageBuilder.ToString();

        Log($"MESSAGE FROM SERVER RECEIVED: {messageFromServer}");

        return messageFromServer;
    }
    
    public void CloseClientSocket()
    {
        _clientSocket.Shutdown(SocketShutdown.Both);
        _clientSocket.Close();
        
        Log($"CLIENT FINISHED");
    }
}