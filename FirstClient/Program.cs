using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using FirstClient;
using FirstClient.NetEngine;
using FirstClient.NetModel;

ClientEngine clientEngine = new ClientEngine("127.0.0.1", 34536);
clientEngine.ConnectToServer();

Wallet wallet = new Wallet()
{
    GoldCoins = 10,
    SilverCoins = 100
};

int action;
Console.WriteLine("1. Инвестировать в золото: ");
Console.WriteLine("2. Инвестировать в серебро: ");

Console.Write("Выберите пункт меню: ");
action = int.Parse(Console.ReadLine());

string command = "";

if (action == 1)
{
    command = Commands.InvestInGoldCoins;
}
else if (action == 2)
{
    command = Commands.InvestInSilverCoins;
}


Request request = new Request()
{
    Command = command,
    JsonData = JsonSerializer.Serialize(wallet)
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