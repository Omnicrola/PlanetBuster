using Assets.Scripts.Balls;
using Assets.Scripts.Util;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(GeneralTestingComponent))]
    public class CustomEditorTesting : UnityEditor.Editor
    {

        public void OnEnable()
        {
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var ballController = target as GeneralTestingComponent;
            var gridPosition = ballController.GridPosition;


            var newGridX = EditorGUILayout.IntField("Grid X", gridPosition.X, GUIStyle.none);
            var newGridY = EditorGUILayout.IntField("Grid Y", gridPosition.Y, GUIStyle.none);

            ballController.GridPosition = new GridPosition(newGridX, newGridY);

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void OnSceneGUI()
        {
            var ballController = target as GeneralTestingComponent;
            Handles.color = Color.yellow;

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.green;

            Vector3 position = ballController.transform.position + Vector3.up * 2f;

            Handles.Label(position, "Grid Position: " + ballController.GridPosition.ToString(), style);

            EditorGUI.BeginChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                Debug.Log("CHANGE");
            }
        }
    }
}