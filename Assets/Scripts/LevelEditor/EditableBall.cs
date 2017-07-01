using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.LevelEditor
{
    public class EditableBall : UnityBehavior
    {
        public BallType BallType;
        public BallMagnitude Magnitude;

        public BallLevelData Export()
        {
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            return new BallLevelData()
            {
                BallType = 1,
                HasPowerGem = false,
                Magnitude = BallMagnitude.Standard,
                XPos = x,
                YPos = y
            };
        }

        public void SetData(BallLevelData ballLevelData)
        {
            transform.position = new Vector3(ballLevelData.XPos, ballLevelData.YPos);
        }
    }
}