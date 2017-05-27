using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public class SimpleObjectPool : UnityBehavior
    {
        public GameObject Prefab;
        private readonly Stack<GameObject> inactiveInstances = new Stack<GameObject>();
        private int cloneId = 0;

        public GameObject GetObjectFromPool()
        {
            if (inactiveInstances.Count > 0)
            {
                var objectFromPool = inactiveInstances.Pop();
                objectFromPool.SetActive(true);
                return objectFromPool;
            }
            else
            {
                var newGameObject = GameObject.Instantiate(Prefab);
                newGameObject.name = newGameObject.name + "(" + (cloneId++) + ")";
                PooledObject pooledObjectComponent = newGameObject.AddComponent<PooledObject>();
                pooledObjectComponent.ObjectPool = this;
                newGameObject.transform.SetParent(transform);
                newGameObject.SetActive(true);
                return newGameObject;
            }
        }

        public void ReturnObjectToPool(GameObject objectToReturn)
        {
            var pooledObject = objectToReturn.GetComponent<PooledObject>();
            if (pooledObject != null && pooledObject.ObjectPool == this)
            {
                objectToReturn.transform.SetParent(null);
                objectToReturn.SetActive(false);
                inactiveInstances.Push(objectToReturn);
            }
            else
            {
                Debug.LogWarning(objectToReturn.name + " was returned to a pool it did not originate from.");
                Destroy(objectToReturn);
            }
        }


    }

    public class InvalidStateException : Exception
    {
        public InvalidStateException(string message) : base(message) { }
    }

    public class PooledObject : MonoBehaviour
    {
        public SimpleObjectPool ObjectPool { get; set; }
    }
}