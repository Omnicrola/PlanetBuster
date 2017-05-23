using UnityEngine;

namespace Assets.SpaceShift_UI_for_UGUI.Scripts.Layout
{
	[AddComponentMenu("Layout/Horizontal Layout Group Extended", 150)]
	public class HorizontalLayoutGroupExtended : HorizontalOrVerticalLayoutGroupExtended
	{
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxisExtended(0, false);
		}
		
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxisExtended(1, false);
		}
		
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxisExtended(0, false);
		}
		
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxisExtended(1, false);
		}
	}
}