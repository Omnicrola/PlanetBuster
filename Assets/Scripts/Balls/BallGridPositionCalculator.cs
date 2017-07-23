using System;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class BallGridPositionCalculator : IBallGridPositionCalculator
    {
        private const float BASE_HORIZONTAL = 0.5f;
        private const float HORZ_SPACING = 0.75f;
        private const float VERT_SPACING = 1f;

        const float NORTH_EAST = 45f;
        const float SOUTH_EAST = 135f;
        const float SOUTH_WEST = 225f;
        const float NORTH_WEST = 315f;

        public GridPosition FindGridPosition(IBallController ballInGrid, BallMagnitude magnitude, float angleOfImpact)
        {
            int offsetX = 0;
            int offsetY = 0;

            var magnitudeScale = magnitude.GetScale();

            if (angleOfImpact >= NORTH_EAST && angleOfImpact < SOUTH_EAST)
            {
                // Right side
                offsetX = (int)(1 * magnitudeScale);
            }
            else if (angleOfImpact >= SOUTH_EAST && angleOfImpact < SOUTH_WEST)
            {
                // Bottom side
                offsetY = (int)(1 * magnitudeScale);
            }
            else if (angleOfImpact >= SOUTH_WEST && angleOfImpact < NORTH_WEST)
            {
                // Left side
                offsetX = -1;
            }
            var gridPosition = ballInGrid.GridPosition;

            return new GridPosition(gridPosition.X + offsetX, gridPosition.Y + offsetY);
        }

        public Vector2 GetWorldPosition(GridPosition gridPosition, Vector3 ceilingOffset, Vector2 offset)
        {
            var x = BASE_HORIZONTAL + (gridPosition.X * HORZ_SPACING);
            float y = 0;
            var isEvenColumn = (gridPosition.X & 1) == 0;
            if (isEvenColumn)
            {
                Console.WriteLine("EVEN: " + gridPosition.X);
                y = (VERT_SPACING * gridPosition.Y) + (VERT_SPACING / 1f);
            }
            else
            {
                Console.WriteLine("ODD: " + gridPosition.X);
                y = (VERT_SPACING * gridPosition.Y) + (VERT_SPACING / 2f);
            }

            var invertedY = y * -1;
            return new Vector2(x, invertedY);
        }

        public GridPosition GetGridPosition(Vector2 worldPosition, Vector3 ceilingOffset, Vector2 offset)
        {
            int x = Mathf.RoundToInt((worldPosition.x - ceilingOffset.x - offset.x) / HORZ_SPACING);
            int y = Mathf.RoundToInt((worldPosition.y - ceilingOffset.y - offset.y) / VERT_SPACING);
            return new GridPosition(x, y);
        }
    }
}