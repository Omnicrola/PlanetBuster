using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class ImportLevelEditorScript : EditorWindow
    {
        [MenuItem("Planet Buster/Import Level")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<ImportLevelEditorScript>();
        }

        void OnGUI()
        {
            GUILayout.Label("Import level", EditorStyles.boldLabel);

            if (GUI.Button(new Rect(10, 50, 100, 32), "Load"))
            {
            }
        }
    }
}