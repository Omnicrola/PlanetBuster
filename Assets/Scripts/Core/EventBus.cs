using System;
using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;

namespace Assets.Scripts.Core
{
    public class EventBus
    {
        public event EventHandler GameOver;
        public event EventHandler GamePrestart;
        public event EventHandler GameStart;

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

        public void BroadcastOrphanedBalls(object source, OrphanedBallsEventArgs orphanedBallsEventArgs)
        {
            var path = orphanedBallsEventArgs.OrphanedBalls
                   .Select(b => b.Model)
                   .Select(m => "(" + m.GridX + "," + m.GridY + ")")
                   .Aggregate((s, e) => s + " " + e);

            Logging.Instance.Log(LogLevel.Info, string.Format("Orphaned balls: " + path));
            if (BallOrphansFound != null)
            {
                BallOrphansFound.Invoke(source, orphanedBallsEventArgs);
            }
        }

        public void BroadcastBallMatch(object source, BallGridMatchArgs matchArgs)
        {
            var path = matchArgs.BallPath
                .Select(b => b.Model)
                .Select(m => "(" + m.GridX + "," + m.GridY + ")")
                .Aggregate((s, e) => s + " -> " + e);

            Logging.Instance.Log(LogLevel.Info, string.Format("Matched set found: " + path));

            if (BallMatchFound != null)
            {
                BallMatchFound.Invoke(source, matchArgs);
            }
        }

        public void BroadcastBallCollision(object source, BallCollisionEventArgs collisionEventArgs)
        {
            Logging.Instance.Log(LogLevel.Info,
                "Collision: " + collisionEventArgs.AngleOfImpact + " " + collisionEventArgs.IncomingBall.Position() +
                "->" + collisionEventArgs.BallInGrid.Position());
            if (BallCollision != null)
            {
                BallCollision.Invoke(source, collisionEventArgs);
            }
        }

        public void BroadcastBallOutOfBounds(object source, BallOutOfBoundsEventArgs ballOutOfBoundsEventArgs)
        {
            Logging.Instance.Log(LogLevel.Debug, "Ball out of bounds: " + ballOutOfBoundsEventArgs.Ball.name);
            if (BallOutOfBounds != null)
            {
                BallOutOfBounds.Invoke(source, ballOutOfBoundsEventArgs);
            }
        }

        public void BroadcastScoreBonusEvent(object source, ScoreBonusEventArgs scoreBonusEventArgs)
        {
            if (ScoreBonus != null)
            {
                ScoreBonus.Invoke(source, scoreBonusEventArgs);
            }
        }

        public void BroadcastGameOver(object source, GameOverEventArgs gameOverEventArgs)
        {
            if (GameOver != null)
            {
                GameOver.Invoke(source, gameOverEventArgs);
            }
        }

        public void BroadcastGamePrestart(object source, EventArgs empty)
        {
            if (GamePrestart != null)
            {
                GamePrestart.Invoke(source, empty);
            }
        }

        public void BroadcastGameStart(object source, EventArgs empty)
        {
            if (GameStart != null)
            {
                GameStart.Invoke(source, empty);
            }
        }
    }
}