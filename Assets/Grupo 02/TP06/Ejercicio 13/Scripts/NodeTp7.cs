public class NodeTp7
{
    public int Score { get; }
    public string PlayerName { get; }

    public NodeTp7 Left { get; set; }
    public NodeTp7 Right { get; set; }
    public int Height { get; set; } = 1;

    public NodeTp7(int score, string playerName)
    {
        Score = score;
        PlayerName = playerName;
    }
}
