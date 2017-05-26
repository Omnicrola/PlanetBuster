namespace Assets.Scripts.Scorekeeping
{
    public class HighScore
    {
        public HighScore(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public string Name { get; private set; }
        public int Score { get; private set; }
    }
}