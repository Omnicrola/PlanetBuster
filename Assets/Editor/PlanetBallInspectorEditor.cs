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

            _typeProperty = serializedObject.FindProperty("BallType");
            _magnitudeProperty = serializedObject.FindProperty("Magnitude");
            _powerGemProperty = serializedObject.FindProperty("HasPowerGem");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_typeProperty);
            EditorGUILayout.PropertyField(_magnitudeProperty);
            EditorGUILayout.PropertyField(_powerGemProperty);

            EditorGUILayout.LabelField("");
            EditorGUILayout.PropertyField(_powerGemSpriteProperty);
            EditorGUILayout.PropertyField(_ballSpriteProperty);
            EditorGUILayout.PropertyField(_damageSpriteProperty);


            serializedObject.ApplyModifiedProperties();
        }

    }

}