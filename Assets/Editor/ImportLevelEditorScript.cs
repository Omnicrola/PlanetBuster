using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class ImportLevelEditorScript : EditorWindow
    {
        private string _currentFile = "";

        [MenuItem("Planet Buster/Import Level")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<ImportLevelEditorScript>();
        }


        void OnGUI()
        {
            GUILayout.Label("Import level", EditorStyles.boldLabel);
            if (GUI.Button(new Rect(10, 50, 100, 20), "Browse..."))
            {
                OpenFileBrowser();
            }
            GUILayout.TextField(_currentFile, 100, EditorStyles.textField);

            if (GUI.Button(new Rect(120, 50, 100, 20), "Load"))
            {
                if (_currentFile == null)
                {
                    Debug.LogError("Cannot import, choose a file first.");
                    return;
                }
                LoadSelectedLevel();
            }
        }

        private void LoadSelectedLevel()
        {
            var importExportFactory = new LevelImportExportFactory();
            var levelImporter = importExportFactory.CreateImporter();
            var wasSuccessful = levelImporter.Import(_currentFile);
            if (wasSuccessful)
            {
                Debug.Log("Successfuly imported level : " + _currentFile);
            }
            else
            {
                Debug.LogWarning("Error, did not load level");
            }
        }

        private void OpenFileBrowser()
        {
            _currentFile = EditorUtility.OpenFilePanel("Load Level", GameConstants.Levels.ResourcePath, "bin");
        }
    }
}