public struct ChestUnlockedMsg
{
    public string title;
    public string description;
    public int coins;
    public int gems;

    public ChestUnlockedMsg(string title, string description, int coins, int gems)
    {
        this.title = title;
        this.description = description;
        this.coins = coins;
        this.gems = gems;
    }
}

