using System.Collections.Generic;
using Assets.Scripts.Balls;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;

namespace Assets.Scripts.Scorekeeping
{
    public class Scorekeeper
    {
        public int CurrentScore { get; private set; }

        public void ScoreOrphans(List<IBallController> orphanedBalls)
        {
            CurrentScore += orphanedBalls.Count * GameConstants.ScorePerBall * 2;
        }

        public void ScoreMatch(List<IBallController> ballPath)
        {
            var ballCount = ballPath.Count;
            CurrentScore += ballCount * GameConstants.ScorePerBall;
            if (ballCount == 4)
            {
                ApplyBonus(GameConstants.BallBonus_4, 4);
            }
            else if (ballCount == 5)
            {
                ApplyBonus(GameConstants.BallBonus_5, 5);
            }
            else if (ballCount == 6)
            {
                ApplyBonus(GameConstants.BallBonus_6, 6);
            }
            else if (ballCount == 7)
            {
                ApplyBonus(GameConstants.BallBonus_7, 7);
            }
            else if (ballCount == 8)
            {
                ApplyBonus(GameConstants.BallBonus_8, 8);
            }
            else if (ballCount == 9)
            {
                ApplyBonus(GameConstants.BallBonus_9, 9);
            }
            else if (ballCount == 10)
            {
                ApplyBonus(GameConstants.BallBonus_10, 10);
            }
        }

        private void ApplyBonus(int scoreBonus, int size)
        {
            CurrentScore += scoreBonus;
            GameManager.Instance.EventBus.Broadcast(new ScoreBonusEventArgs(size, scoreBonus));
        }

        public void ScoreDestroyedBall()
        {
            CurrentScore += GameConstants.ScorePerBall;
        }
    }
}