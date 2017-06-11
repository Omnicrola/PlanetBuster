using System;

namespace Assets.Scripts.LevelEditor
{
    [Serializable]
    public class BallExportModel
    {
        public string SpriteName;
        public GridLocation gridLocation;

        public BallExportModel(GridLocation gridLocation)
        {
            this.gridLocation = gridLocation;
        }
    }

    [Serializable]
    public struct GridLocation
    {
        public readonly int X;
        public readonly int Y;
        public GridLocation(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}