using Assets.Scripts.Core.Levels;
using UnityEditor;

namespace Assets.Editor
{
    [CustomEditor(typeof(LevelDataController))]
    public class LevelDataInspectorEditor : UnityEditor.Editor
    {
        private SerializedProperty _levelNumberProperty;
        private SerializedProperty _levelNameProperty;

        public void OnEnable()
        {
            _levelNumberProperty = serializedObject.FindProperty("LevelNumber");
            _levelNameProperty = serializedObject.FindProperty("LevelName");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_levelNumberProperty);
            EditorGUILayout.PropertyField(_levelNameProperty);


            serializedObject.ApplyModifiedProperties();
        }
    }
}