using System;
using UnityEngine;

namespace Assets.SpaceShift_UI_for_UGUI.Scripts
{
	[Serializable]
	public class UIItemInfo
	{
		public int ID;
		public string Name;
		public Sprite Icon;
		public string Description;
		public UIEquipmentType EquipType;
		public int ItemType;
		public string Type;
		public string Subtype;
		public int Damage;
		public float AttackSpeed;
		public int Block;
		public int Armor;
		public int Stamina;
		public int Strength;
	}
}