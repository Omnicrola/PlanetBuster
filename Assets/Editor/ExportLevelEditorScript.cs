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
            var levelImportExportFactory = new LevelImportExportFactory();
            var exporter = levelImportExportFactory.CreateExporter();
            var exportResult = exporter.Export();
            if (exportResult)
            {
                Debug.Log("Successfully exported level.");
            }
            else
            {
                Debug.LogWarning("Export failed!");
            }
        }
    }
}