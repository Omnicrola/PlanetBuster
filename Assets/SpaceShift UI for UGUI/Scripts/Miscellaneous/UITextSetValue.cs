﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.SpaceShift_UI_for_UGUI.Scripts.Miscellaneous
{
	[RequireComponent(typeof(Text))]
	public class UITextSetValue : MonoBehaviour {
		
		private Text m_Text;
		public string floatFormat = "0.00";
		
		protected void Awake()
		{
			this.m_Text = this.gameObject.GetComponent<Text>();
		}
		
		public void SetFloat(float value)
		{
			if (this.m_Text != null)
				this.m_Text.text = value.ToString(floatFormat);
		}
	}
}