using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

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
                return inactiveInstances.Pop();
            }
            else
            {
                var newGameObject = GameObject.Instantiate(Prefab);
                newGameObject.name = newGameObject.name + "(" + (cloneId++) + ")";
                PooledObject pooledObjectComponent = newGameObject.AddComponent<PooledObject>();
                pooledObjectComponent.pool = this;
                newGameObject.transform.SetParent(transform);
                newGameObject.SetActive(true);
                return newGameObject;
            }
        }

        public void ReturnObjectToPool(GameObject objectToReturn)
        {
            var pooledObject = objectToReturn.GetComponent<PooledObject>();
            if (pooledObject != null && pooledObject.pool == this)
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
        public SimpleObjectPool pool;
    }
}