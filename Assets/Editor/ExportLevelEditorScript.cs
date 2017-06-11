using Assets.Scripts;
using Assets.Scripts.LevelEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Editor
{
    public class ExportLevelEditorScript : EditorWindow
    {
        [MenuItem("Planet Buster/Export Level")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<ExportLevelEditorScript>();
        }

        void OnGUI()
        {
            GUILayout.Label("Export level", EditorStyles.boldLabel);
            GUILayout.Label("Level name: ", EditorStyles.boldLabel);
            if (GUI.Button(new Rect(10, 50, 100, 32), "Export"))
            {
                RunExport();
            }
        }

        private void RunExport()
        {
            var levelEditorIsNotTheCurrentScene = SceneManager.GetActiveScene().name !=
                                                  GameConstants.SceneNames.LevelEditor;

            if (levelEditorIsNotTheCurrentScene)
            {
                Debug.LogError("Error : Cannot export from any scene except " + GameConstants.SceneNames.LevelEditor);
                return;
            }
            var gridEditorObject = GameObject.Find("GridEditorSettings");
            if (gridEditorObject == null)
            {
                Debug.LogError("Cannot export, there is no object in scene named : 'GridEditorSettings'");
                return;
            }
            var gridEditorSettings = gridEditorObject.GetComponent<IGridEditorSettings>();
            if (gridEditorSettings == null)
            {
                Debug.LogError("Cannot export, the GridEditorSettings component is not loaded");
            }

            var levelExporter = new LevelExporter(gridEditorSettings);

            var exportResult = levelExporter.Export();
            if (exportResult)
            {
                Debug.Log("Successfully exported level.");
            }
        }
    }
}