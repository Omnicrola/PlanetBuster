using UnityEngine;

namespace Assets.SpaceShift_UI_for_UGUI.Scripts.Miscellaneous
{
    [ExecuteInEditMode]
    public class UIMirrorDimensions : MonoBehaviour
    {
        [SerializeField] private RectTransform m_Target;

        protected void OnRectTransformDimensionsChange()
        {
            if (this.isActiveAndEnabled && this.m_Target != null)
            {
                RectTransform trans = this.transform as RectTransform;
                this.m_Target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, trans.rect.width);
                this.m_Target.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, trans.rect.height);
            }
        }
    }
}