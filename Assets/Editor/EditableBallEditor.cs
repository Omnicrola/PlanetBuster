using System;
using Assets.Scripts.LevelEditor;
using Assets.Scripts.Models;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(EditableBall))]
    [CanEditMultipleObjects]
    public class EditableBallEditor : UnityEditor.Editor
    {
        private SerializedProperty _typeProperty;
        private SerializedProperty _magnitudeProperty;

        public void OnEnable()
        {
            _typeProperty = serializedObject.FindProperty("BallType");
            _magnitudeProperty = serializedObject.FindProperty("Magnitude");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_typeProperty);
            EditorGUILayout.PropertyField(_magnitudeProperty);


            serializedObject.ApplyModifiedProperties();
        }

        public void OnSceneGUI()
        {
            // Implement what you want to see in scene view here
        }
    }

}