using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Core.Levels;
using Assets.Scripts.Extensions;
using UnityEditor;
using UnityEngine;

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
        private SerializedProperty _ballPrefabProperty;

        public void OnEnable()
        {
            _levelNumberProperty = serializedObject.FindProperty("LevelNumber");
            _levelNameProperty = serializedObject.FindProperty("LevelName");
            _showPowerbarProperty = serializedObject.FindProperty("ShowPowerBar");
            _startMessageProperty = serializedObject.FindProperty("StartMessage");
            _ballSequenceProperty = serializedObject.FindProperty("i_BallSequence");
            _ballPrefabProperty = serializedObject.FindProperty("BallPrefab");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_levelNumberProperty);
            EditorGUILayout.PropertyField(_levelNameProperty);
            EditorGUILayout.PropertyField(_showPowerbarProperty);
            EditorGUILayout.PropertyField(_startMessageProperty);
            EditorGUILayout.PropertyField(_ballSequenceProperty);

            if (GUILayout.Button("Regenerate"))
            {

                var ballGridPositionCalculator = new BallGridPositionCalculator();
                var parentTransform = (target as LevelDataController).transform;
                var container = (target as LevelDataController).gameObject;
                var allChildBalls = container.GetChildren().Where(c => c.GetComponent<IBallController>() != null).ToList();
                foreach (var childBall in allChildBalls)
                {
                    DestroyImmediate(childBall);
                }
                for (int x = 0; x < 11; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        var newBall = GameObject.Instantiate(_ballPrefabProperty.objectReferenceValue as GameObject);
                        var position = ballGridPositionCalculator.GetWorldPosition(new GridPosition(x, y), Vector3.zero,
                            Vector2.zero);
                        newBall.GetComponent<IBallController>().MarkDirty();
                        newBall.transform.position = position;
                        newBall.transform.SetParent(parentTransform);
                    }
                }
            }
            EditorGUILayout.PropertyField(_ballPrefabProperty);

            serializedObject.ApplyModifiedProperties();
        }
    }
}