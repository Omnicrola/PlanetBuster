using UnityEngine;

namespace Assets.SpaceShift_UI_for_UGUI.Scripts.Miscellaneous
{
    public class BitMaskAttribute : PropertyAttribute
    {
        public System.Type propType;
	
        public BitMaskAttribute(System.Type aType)
        {
            propType = aType;
        }
    }
}
