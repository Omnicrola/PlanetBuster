﻿using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class AngleOfImpactCalculator
    {
        public float Calculate(Vector3 projectilePosition, Vector3 gridBallPosition)
        {
            projectilePosition.z = 0;
            gridBallPosition.z = 0;
            var difference = gridBallPosition - projectilePosition;
            var angle = Vector3.Angle(difference, Vector3.down);
            angle = difference.x > 0 ? 360 - angle : angle;
            return angle;
        }
    }
}