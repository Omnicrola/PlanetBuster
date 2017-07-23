using System;

namespace Assets.Scripts.Statistics
{
    public interface IStatsManager
    {
        TimeSpan ElapsedTime { get; }
        int TotalScore { get; }
        int BallsLaunched { get; }
        int TotalMatches { get; }
        int LargestMatch { get; }
    }
}