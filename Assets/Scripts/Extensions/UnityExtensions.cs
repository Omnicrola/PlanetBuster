using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Extensions
{
    public static class UnityExtensions
    {
        public static Random _random = new Random();


        public static Vector3 Translate(this Vector3 vec, float x, float y, float z)
        {
            return new Vector3(vec.x + x, vec.y + y, vec.z + z);
        }

        public static Vector3 RandomOffset(this Vector3 vec, float maximumOffset)
        {
            float x = (float)_random.NextDouble() * maximumOffset * 2 - maximumOffset;
            float y = (float)_random.NextDouble() * maximumOffset * 2 - maximumOffset;
            float z = (float)_random.NextDouble() * maximumOffset * 2 - maximumOffset;
            return new Vector3(vec.x + x, vec.y + y, vec.z + z);
        }

        public static Vector2 RandomOffset(this Vector2 vec, float maximumOffset)
        {
            float x = (float)(_random.NextDouble() * maximumOffset * 2) - maximumOffset;
            float y = (float)(_random.NextDouble() * maximumOffset * 2) - maximumOffset;
            return new Vector2(vec.x + x, vec.y + y);
        }

        public static Vector3 RandomVector(this Random random, float maximum)
        {
            float x = (float)(random.NextDouble() * maximum * 2) - maximum;
            float y = (float)(random.NextDouble() * maximum * 2) - maximum;
            float z = (float)(random.NextDouble() * maximum * 2) - maximum;
            return new Vector3(x, y, z);
        }

        public static List<GameObject> GetChildren(this GameObject gameObject)
        {
            var gameObjects = new List<GameObject>();
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var childTransform = gameObject.transform.GetChild(i);
                gameObjects.Add(childTransform.gameObject);
            }
            return gameObjects;
        }
    }
}