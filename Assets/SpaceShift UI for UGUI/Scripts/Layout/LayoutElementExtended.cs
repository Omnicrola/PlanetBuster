﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.SpaceShift_UI_for_UGUI.Scripts.Layout
{
	[AddComponentMenu("Layout/Layout Element Extended", 140), ExecuteInEditMode, RequireComponent(typeof(RectTransform))]
	public class LayoutElementExtended : LayoutElement, ILayoutElementExtended
	{
		[SerializeField] private RectOffset m_Margin = new RectOffset();
		
		public virtual RectOffset margin
		{
			get
			{
				return this.m_Margin;
			}
			set
			{
				this.m_Margin = value;
				base.SetDirty();
			}
		}
		
		public override void CalculateLayoutInputHorizontal()
		{
		}
		
		public override void CalculateLayoutInputVertical()
		{
		}
	}
}