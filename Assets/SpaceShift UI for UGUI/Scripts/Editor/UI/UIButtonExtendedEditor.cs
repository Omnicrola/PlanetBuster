using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;
using System.Collections;
using Assets.SpaceShift_UI_for_UGUI.Scripts.UI;

namespace UnityEditor.UI
{
	[CanEditMultipleObjects, CustomEditor(typeof(UIButtonExtended), true)]
	public class UIButtonExtendedEditor : ButtonEditor {
	
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			serializedObject.Update();
			EditorGUILayout.Separator();
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onStateChange"), new GUIContent("On State Change"), true);
			serializedObject.ApplyModifiedProperties();
		}
	}
}