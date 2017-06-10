using UnityEngine;

namespace Assets.Scripts.LevelEditor
{
    public struct GridPosition
    {
        public readonly int X;
        public readonly int Y;
        public readonly Vector2 WorldPosition;

        public GridPosition(int x, int y, Vector2 worldPosition)
        {
            X = x;
            Y = y;
            WorldPosition = worldPosition;
        }
    }
}