using System;
using Assets.Scripts.Balls;
using Assets.Scripts.LevelEditor;
using Assets.Scripts.Models;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(BallController))]
    [CanEditMultipleObjects]
    public class PlanetBallInspectorEditor : UnityEditor.Editor
    {
        private SerializedProperty _typeProperty;
        private SerializedProperty _magnitudeProperty;
        private SerializedProperty _powerGemProperty;

        private SerializedProperty _powerGemSpriteProperty;
        private SerializedProperty _ballSpriteProperty;
        private SerializedProperty _damageSpriteProperty;

        public void OnEnable()
        {
            _powerGemSpriteProperty = serializedObject.FindProperty("PowerGemSprite");
            _ballSpriteProperty = serializedObject.FindProperty("BallSprite");
            _damageSpriteProperty = serializedObject.FindProperty("DamageSprite");

            _typeProperty = serializedObject.FindProperty("i_BallType");
            _magnitudeProperty = serializedObject.FindProperty("i_Magnitude");
            _powerGemProperty = serializedObject.FindProperty("i_HasPowerGem");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_typeProperty, new GUIContent("Type"));
            EditorGUILayout.PropertyField(_magnitudeProperty, new GUIContent("Magnitude"));
            EditorGUILayout.PropertyField(_powerGemProperty, new GUIContent("Has Power Gem"));

            EditorGUILayout.LabelField("");
            EditorGUILayout.PropertyField(_powerGemSpriteProperty);
            EditorGUILayout.PropertyField(_ballSpriteProperty);
            EditorGUILayout.PropertyField(_damageSpriteProperty);


            serializedObject.ApplyModifiedProperties();
            foreach (var singleTarget in targets)
            {
                var ballController = singleTarget as BallController;
                ballController.MarkDirty();
            }
        }

        void OnSceneGui()
        {
            var handleExample = target as BallController;
            Handles.color = Color.yellow;

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.green;

            Vector3 position = handleExample.transform.position + Vector3.up * 2f;
            string posString = position.ToString();

            Handles.Label(position,
                posString + "\nShieldArea: " +
                handleExample.shieldArea.ToString(),
                style
                );

            Handles.BeginGUI();
            if (GUILayout.Button("Reset Area", GUILayout.Width(100)))
            {
                handleExample.shieldArea = 5;
            }
            Handles.EndGUI();

            Handles.DrawWireArc(
                handleExample.transform.position,
                handleExample.transform.up,
                -handleExample.transform.right,
                180,
                handleExample.shieldArea);

            handleExample.shieldArea =
                Handles.ScaleValueHandle(handleExample.shieldArea,
                    handleExample.transform.position + handleExample.transform.forward * handleExample.shieldArea,
                    handleExample.transform.rotation,
                    1, Handles.ConeHandleCap, 1);
        }

    }

}