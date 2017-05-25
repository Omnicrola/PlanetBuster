using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Balls
{
    public interface IBallPath : IEnumerable<IBallController>
    {
        int Count { get; }
    }
}