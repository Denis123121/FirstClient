namespace FirstClient;

public class Wallet
{
    public int GoldCoins { get; set; }
    public int SilverCoins { get; set; }

    public override string ToString()
    {
        return $"GoldCoins = {GoldCoins}, SilverCoins = {SilverCoins}";
    }
}