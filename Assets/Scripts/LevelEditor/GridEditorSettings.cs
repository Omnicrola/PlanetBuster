using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.LevelEditor
{
    public class GridEditorSettings : UnityBehavior, IGridEditorSettings
    {
        public GameObject Ceiling;
        public string LevelName;
        public int LevelNumber;

        public GridLocation CalculateGridLocation(Transform ballTransform)
        {
            int x = (int)ballTransform.position.x;
            int y = (int)ballTransform.position.y;
            return new GridLocation(x, y);
        }

        public LevelData GetExportData()
        {
            return new LevelData()
            {
                LevelName = LevelName,
                Order = LevelNumber
            };
        }
    }
}