using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using FirstClient;
using FirstClient.NetEngine;
using FirstClient.NetModel;

ClientEngine clientEngine = new ClientEngine("127.0.0.1", 34536);
clientEngine.ConnectToServer();

Wallet shop = new Wallet()
{
    GoldCoins = 10,
    SilverCoins = 100
};

Request request = new Request()
{
    Command = Commands.AddAge,
    JsonData = JsonSerializer.Serialize(shop)
};

string messageToServer = JsonSerializer.Serialize(request);

clientEngine.SendMessage(messageToServer);
string messageFromServer = clientEngine.ReceiveMessage();

Response response = JsonSerializer.Deserialize<Response>(messageFromServer);

if (response.Status == Statuses.Ok)
{
    Wallet receivedWallet = JsonSerializer.Deserialize<Wallet>(response.JsonData);
    Console.WriteLine(receivedWallet);
}
else
{
    Console.WriteLine($"Something wrong Status = {response.Status}");
}

clientEngine.CloseClientSocket();