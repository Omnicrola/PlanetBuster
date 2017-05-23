using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using Assets.SpaceShift_UI_for_UGUI.Scripts.UI.Select_Field;

namespace UnityEditor.UI
{
	[CanEditMultipleObjects, CustomEditor(typeof(UISelectField_List), true)]
	public class UISelectField_ListEditor : Editor {

		public override void OnInspectorGUI()
		{
		}
	}
}