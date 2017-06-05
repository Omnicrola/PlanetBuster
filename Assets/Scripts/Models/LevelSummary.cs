namespace Assets.Scripts.Models
{
    public class LevelSummary
    {
        public LevelSummary(string levelNumber)
        {
            LevelNumber = levelNumber;
        }

        public bool IsLocked { get; set; }
        public string LevelNumber { get; private set; }
    }
}