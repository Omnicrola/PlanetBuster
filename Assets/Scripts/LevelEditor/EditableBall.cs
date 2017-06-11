using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.LevelEditor
{
    public class EditableBall : UnityBehavior
    {
        public BallExportModel Export()
        {
            var gridSettings = GameObject.Find("GridEditorSettings");
            var gridEditorSettings = gridSettings.GetComponent<GridEditorSettings>();
            var gridLocation = gridEditorSettings.CalculateGridLocation(transform);

            var spriteName = GetComponent<SpriteRenderer>().sprite.name;
            return new BallExportModel(gridLocation)
            {
                SpriteName = spriteName
            };
        }
    }
}