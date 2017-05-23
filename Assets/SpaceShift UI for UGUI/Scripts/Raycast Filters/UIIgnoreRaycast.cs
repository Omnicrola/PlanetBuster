using UnityEngine;

namespace Assets.SpaceShift_UI_for_UGUI.Scripts.Raycast_Filters
{
	[AddComponentMenu("UI/Raycast Filters/Ignore Raycast Filter")]
	public class UIIgnoreRaycast : MonoBehaviour, ICanvasRaycastFilter 
	{
		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return false;
		}
	}
}
