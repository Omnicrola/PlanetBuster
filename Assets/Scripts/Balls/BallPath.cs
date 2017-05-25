using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Balls
{
    public class BallPath : IBallPath
    {
        public int Count
        {
            get { return _balls.Count; }
        }

        private readonly List<IBallController> _balls;

        public BallPath()
        {
            _balls = new List<IBallController>();
        }

        public void Append(IBallController ballController)
        {
            _balls.Add(ballController);
        }

        IEnumerator<IBallController> IEnumerable<IBallController>.GetEnumerator()
        {
            return _balls.GetEnumerator();
        }


        public IEnumerator GetEnumerator()
        {
            return _balls.GetEnumerator();
        }
    }
}