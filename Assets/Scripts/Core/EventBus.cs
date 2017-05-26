using System;
using Assets.Scripts.Balls;
using Assets.Scripts.Core.Events;

namespace Assets.Scripts.Core
{
    public class EventBus
    {
        public event EventHandler<ScoreChangedEventArgs> ScoreChanged;
        public event EventHandler<OrphanedBallsEventArgs> BallOrphansFound;

        public event EventHandler<BallGridMatchArgs> BallMatchFound;
        public event EventHandler<BallCollisionEventArgs> BallCollision;
        public event EventHandler<BallOutOfBoundsEventArgs> BallOutOfBounds;

        public event EventHandler<ScoreBonusEventArgs> ScoreBonus;

        public void BroadcastScoreChanged(object source, ScoreChangedEventArgs scoreChangeArgs)
        {
            if (ScoreChanged != null)
            {
                ScoreChanged.Invoke(source, scoreChangeArgs);
            }
        }

        public void BroadcastOrphanedBalls(object source, OrphanedBallsEventArgs scoreChangeArgs)
        {
            if (BallOrphansFound != null)
            {
                BallOrphansFound.Invoke(source, scoreChangeArgs);
            }
        }

        public void BroadcastBallMatch(object source, BallGridMatchArgs scoreChangeArgs)
        {
            if (BallMatchFound != null)
            {
                BallMatchFound.Invoke(source, scoreChangeArgs);
            }
        }

        public void BroadcastBallCollision(object source, BallCollisionEventArgs scoreChangeArgs)
        {
            if (BallCollision != null)
            {
                BallCollision.Invoke(source, scoreChangeArgs);
            }
        }

        public void BroadcastBallOutOfBounds(object source, BallOutOfBoundsEventArgs ballOutOfBoundsEventArgs)
        {
            if (BallOutOfBounds != null)
            {
                BallOutOfBounds.Invoke(source, ballOutOfBoundsEventArgs);
            }
        }

        public void BroadcastScorBonusEvent(object source, ScoreBonusEventArgs scoreBonusEventArgs)
        {
            if (ScoreBonus != null)
            {
                ScoreBonus.Invoke(source, scoreBonusEventArgs);
            }
        }
    }
}