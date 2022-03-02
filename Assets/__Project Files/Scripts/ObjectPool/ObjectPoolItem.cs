
using UnityEngine;

namespace Nasser.io
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        public int amountToPool;
        public GameObject[] objectToPool;
        public bool shouldExpand;
    }
}
