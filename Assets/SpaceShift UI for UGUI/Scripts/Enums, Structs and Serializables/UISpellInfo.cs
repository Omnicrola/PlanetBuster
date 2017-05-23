using System;
using Assets.SpaceShift_UI_for_UGUI.Scripts.Miscellaneous;
using UnityEngine;

namespace Assets.SpaceShift_UI_for_UGUI.Scripts
{
	[Serializable]
	public class UISpellInfo
	{
		public int ID;
		public string Name;
		public Sprite Icon;
		public string Description;
		public float Range;
		public float Cooldown;
		public float CastTime;
		public float PowerCost;
	
		[BitMask(typeof(UISpellInfo_Flags))]
		public UISpellInfo_Flags Flags;
	}
}