using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

#endif

namespace Assets.SpaceShift_UI_for_UGUI.Scripts.Miscellaneous
{
    [ExecuteInEditMode]
    public class BreakPrefabConnection : MonoBehaviour
    {
        void Start()
        {
#if UNITY_EDITOR
            PrefabUtility.DisconnectPrefabInstance(gameObject);
#endif
            DestroyImmediate(this); // Remove this script
        }
    }
}