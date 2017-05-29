using System;
using Assets.Scripts.Core.Events;

namespace Assets.Scripts.Core
{
    public interface IGameEventBus
    {
        event EventHandler GameOver;
        event EventHandler GamePrestart;
        event EventHandler GameStart;
        event EventHandler<ScoreChangedEventArgs> ScoreChanged;
        event EventHandler<OrphanedBallsEventArgs> BallOrphansFound;
        event EventHandler<BallGridMatchArgs> BallMatchFound;
        event EventHandler<BallCollisionEventArgs> BallCollision;
        event EventHandler<BallOutOfBoundsEventArgs> BallOutOfBounds;
        event EventHandler<ScoreBonusEventArgs> ScoreBonus;
        event EventHandler BallFired;
        void BroadcastScoreChanged(object source, ScoreChangedEventArgs scoreChangeArgs);
        void BroadcastOrphanedBalls(object source, OrphanedBallsEventArgs orphanedBallsEventArgs);
        void BroadcastBallMatch(object source, BallGridMatchArgs matchArgs);
        void BroadcastBallCollision(object source, BallCollisionEventArgs collisionEventArgs);
        void BroadcastBallOutOfBounds(object source, BallOutOfBoundsEventArgs ballOutOfBoundsEventArgs);
        void BroadcastScoreBonusEvent(object source, ScoreBonusEventArgs scoreBonusEventArgs);
        void BroadcastGameOver(object source, GameOverEventArgs gameOverEventArgs);
        void BroadcastGamePrestart(object source, EventArgs empty);
        void BroadcastGameStart(object source, EventArgs empty);
        void BroadcastBallFired(object source, EventArgs empty);
    }
}