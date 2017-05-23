using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class UnityExtensions
    {
        public static Vector3 Add(this Vector3 vec, float x, float y, float z)
        {
            return new Vector3(vec.x + x, vec.y + y, vec.z + z);
        }
    }
}