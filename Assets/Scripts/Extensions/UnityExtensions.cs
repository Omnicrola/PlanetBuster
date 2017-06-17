using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Extensions
{
    public static class UnityExtensions
    {
        public static Vector3 Add(this Vector3 vec, float x, float y, float z)
        {
            return new Vector3(vec.x + x, vec.y + y, vec.z + z);
        }

        public static Vector3 RandomVector(this Random random, float maximum)
        {
            float x = (float)random.NextDouble() * maximum;
            float y = (float)random.NextDouble() * maximum;
            float z = (float)random.NextDouble() * maximum;
            return new Vector3(x, y, z);
        }
    }
}