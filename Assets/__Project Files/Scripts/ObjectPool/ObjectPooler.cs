using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nasser.io
{
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler SharedInstance;

        [SerializeField] List<GameObject> pooledObjects;
        [SerializeField] List<ObjectPoolItem> itemsToPool;


        private GameObject obj;
        private int randIndex;
        void Awake()
        {
            SharedInstance = this;
        }
        void Start()
        {
            pooledObjects = new List<GameObject>();
            foreach (ObjectPoolItem item in itemsToPool)
            {
                for (int i = 0; i < item.amountToPool; i++)
                {
                    randIndex = Random.Range(0, item.objectToPool.Length);

                    
                    obj = Instantiate(item.objectToPool[randIndex]);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
            }
        }

        public GameObject GetPooledObject(string tag)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag))
                {
                    return pooledObjects[i];
                }
            }
            foreach (ObjectPoolItem item in itemsToPool)
            {
                randIndex = Random.Range(0, item.objectToPool.Length);
                if (item.objectToPool[randIndex].CompareTag(tag))
                {
                    if (item.shouldExpand)
                    {
                        obj = Instantiate(item.objectToPool[randIndex]);
                        obj.SetActive(false);
                        pooledObjects.Add(obj);
                        return obj;
                    }
                }
            }
            return null;
        }

    }
}
