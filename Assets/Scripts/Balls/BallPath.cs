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

        private readonly List<BallController> _balls;

        public BallPath()
        {
            _balls = new List<BallController>();
        }

        public void Append(BallController ballController)
        {
            _balls.Add(ballController);
        }

        IEnumerator<BallController> IEnumerable<BallController>.GetEnumerator()
        {
            return _balls.GetEnumerator();
        }


        public IEnumerator GetEnumerator()
        {
            return _balls.GetEnumerator();
        }
    }
}