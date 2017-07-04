using Assets.Scripts.LevelEditor;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(GridEditorSettings))]
    public class LevelGridEditor : UnityEditor.Editor
    {
        private SerializedProperty _levelNameProperty;
        private SerializedProperty _levelNumberProperty;

        public void OnEnable()
        {
            _levelNameProperty = serializedObject.FindProperty("LevelName");
            _levelNumberProperty = serializedObject.FindProperty("LevelNumber");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_levelNumberProperty);
            EditorGUILayout.PropertyField(_levelNameProperty);

            if (GUILayout.Button("Align All Balls"))
            {
                var gridEditorSettings = target as GridEditorSettings;
                gridEditorSettings.AlignAllChildrenToGrid();
            }
        }

        public void OnSceneGUI()
        {
        }
    }
}