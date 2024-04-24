using UnityEngine;

namespace U3.ObjectPool
{
    internal class RetrievingObjectPool : IObjectPool
    {
        private int curentIndex;
        private readonly int maxIndex;
        private readonly GameObject[] pooledObjects;

        private GameObject RetrieveObject()
        {
            curentIndex = 0;
            pooledObjects[0].SetActive(false);
            return pooledObjects[0];
        }

        public GameObject GetObject()
        {
            if (curentIndex >= maxIndex)
                return RetrieveObject();

            GameObject obj = pooledObjects[curentIndex];
            curentIndex++;

            obj.SetActive(false);
            return obj;
        }

        public bool AddObject(GameObject obj) => true;

        public RetrievingObjectPool(ObjectPoolSetting poolSetting)
        {
            pooledObjects = new GameObject[poolSetting.StartSize];
            for (int i = 0; i < poolSetting.StartSize; i++)
            {
                pooledObjects[i] = Object.Instantiate(poolSetting.Object);
                pooledObjects[i].SetActive(false);
            }

            maxIndex = poolSetting.StartSize - 1;
        }
    }
}
