using System;
using Assets.Scripts;
using Assets.Scripts.LevelEditor;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Editor
{
    public class LevelImportExportFactory
    {

        public LevelExporter CreateExporter()
        {
            var gridEditorSettings = GetGridEditorSettings();
            return new LevelExporter(gridEditorSettings);
        }

        private static IGridEditorSettings GetGridEditorSettings()
        {
            var levelEditorIsNotTheCurrentScene = SceneManager.GetActiveScene().name !=
                                                  GameConstants.SceneNames.LevelEditor;

            if (levelEditorIsNotTheCurrentScene)
            {
                Debug.LogWarning("Error : Cannot export from any scene except " +
                                                GameConstants.SceneNames.LevelEditor);
                return null;
            }
            var gridEditorObject = GameObject.Find("GridEditorSettings");
            if (gridEditorObject == null)
            {
                Debug.LogWarning("Cannot export, there is no object in scene named : 'GridEditorSettings'");
                return null;
            }
            var gridEditorSettings = gridEditorObject.GetComponent<IGridEditorSettings>();
            if (gridEditorSettings == null)
            {
                Debug.LogWarning("Cannot export, the GridEditorSettings component is not loaded");
                return null;
            }
            return gridEditorSettings;
        }

        public LevelImporter CreateImporter()
        {
            var gridEditorSettings = GetGridEditorSettings();
            return new LevelImporter(gridEditorSettings);
        }
    }
}