using System;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public struct GridPosition
    {
        public static readonly GridPosition Invalid = new GridPosition(-1, -1);
        public static readonly GridPosition Zero = new GridPosition(0, 0);
        public int X;
        public int Y;

        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(GridPosition other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is GridPosition && Equals((GridPosition)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString()
        {
            return "GridPosition[" + X + ", " + Y + "]";
        }

        private const float BASE_HORIZONTAL = 0.5f;
        private const float HORZ_SPACING = 0.75f;
        private const float VERT_SPACING = 1f;
        public Vector2 ToWorldPosition()
        {
            var worldX = BASE_HORIZONTAL + (X * HORZ_SPACING);
            float worldY = 0;
            var isEvenColumn = (X & 1) == 0;
            if (isEvenColumn)
            {
                Console.WriteLine("EVEN: " + X);
                worldY = (VERT_SPACING * Y) + (VERT_SPACING / 1f);
            }
            else
            {
                Console.WriteLine("ODD: " + X);
                worldY = (VERT_SPACING * Y) + (VERT_SPACING / 2f);
            }

            var invertedY = worldY * -1;
            return new Vector2(worldX, invertedY);
        }
    }
}