using System;
using System.Resources;
using UnityEngine;

namespace Assets.Scripts.Models
{
    [Serializable]
    public enum BallType
    {
        Purple,
        Red,
        Green,
        Blue
    }

    public static class BallTypeExtension
    {
        public static Sprite GetSprite(this BallType ballType)
        {
            switch (ballType)
            {
                case BallType.Purple:
                    return Resources.Load<Sprite>(GameConstants.Balls.PurpleBallSprite);
                    break;
                case BallType.Red:
                    return Resources.Load<Sprite>(GameConstants.Balls.RedBallSprite);
                    break;
                case BallType.Green:
                    return Resources.Load<Sprite>(GameConstants.Balls.GreenBallSprite);
                    break;
                case BallType.Blue:
                    return Resources.Load<Sprite>(GameConstants.Balls.BlueBallSprite);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("ballType", ballType, null);
            }
        }
    }
}