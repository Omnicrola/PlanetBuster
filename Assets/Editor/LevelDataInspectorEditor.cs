using Assets.Scripts.Core.Levels;
using UnityEditor;

namespace Assets.Editor
{
    [CustomEditor(typeof(LevelDataController))]
    public class LevelDataInspectorEditor : UnityEditor.Editor
    {
        private SerializedProperty _levelNumberProperty;
        private SerializedProperty _levelNameProperty;
        private SerializedProperty _showPowerbarProperty;
        private SerializedProperty _startMessageProperty;
        private SerializedProperty _ballSequenceProperty;

        public void OnEnable()
        {
            _levelNumberProperty = serializedObject.FindProperty("LevelNumber");
            _levelNameProperty = serializedObject.FindProperty("LevelName");
            _showPowerbarProperty = serializedObject.FindProperty("ShowPowerBar");
            _startMessageProperty = serializedObject.FindProperty("StartMessage");
            _ballSequenceProperty = serializedObject.FindProperty("i_BallSequence");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_levelNumberProperty);
            EditorGUILayout.PropertyField(_levelNameProperty);
            EditorGUILayout.PropertyField(_showPowerbarProperty);
            EditorGUILayout.PropertyField(_startMessageProperty);
            EditorGUILayout.PropertyField(_ballSequenceProperty);


            serializedObject.ApplyModifiedProperties();
        }
    }
}