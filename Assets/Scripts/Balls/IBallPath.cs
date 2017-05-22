using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Balls
{
    public interface IBallPath : IEnumerable<BallController>
    {
        int Count { get; }
    }
}