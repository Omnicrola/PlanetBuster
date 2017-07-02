using Assets.Scripts.LevelEditor;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(EditableBall))]
    [CanEditMultipleObjects()]
    public class EditableBallEditor : UnityEditor.Editor
    {


        private SerializedObject obj;
        private SerializedProperty _typeProperty;
        private SerializedProperty _magnitudeProperty;

        public void OnEnable()
        {
            obj = new SerializedObject(target);
            _typeProperty = obj.FindProperty("BallType");
            _magnitudeProperty = obj.FindProperty("Magnitude");
        }

        public override void OnInspectorGUI()
        {
            obj.Update();
            GUIStyle style = new GUIStyle();
            style.font = EditorStyles.boldFont;
            EditorGUILayout.LabelField("Editable Ball", style, null);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Type");
            var typeIndex = EditorGUILayout.Popup(_typeProperty.enumValueIndex, _typeProperty.enumDisplayNames);
            EditorGUILayout.EndHorizontal();

            _magnitudeProperty.intValue = typeIndex;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Magnitude");
            var magnitudeIndex = EditorGUILayout.Popup(_magnitudeProperty.enumValueIndex, _magnitudeProperty.enumDisplayNames);
            EditorGUILayout.EndHorizontal();

            _magnitudeProperty.intValue = magnitudeIndex;

            obj.ApplyModifiedProperties();
        }

        public void OnSceneGUI()
        {
            // Implement what you want to see in scene view here
        }
    }

}