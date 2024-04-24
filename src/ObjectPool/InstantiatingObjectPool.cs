using UnityEngine;

namespace U3.ObjectPool
{
    internal class InstantiatingObjectPool : IObjectPool
    {
        private readonly GameObject[] pooledObjects;

        public bool AddObject(GameObject obj)
        {
            throw new System.NotImplementedException();
        }

        public GameObject GetObject()
        {
            throw new System.NotImplementedException();
        }

        public InstantiatingObjectPool(ObjectPoolSetting poolSetting)
        {
            pooledObjects = new GameObject[poolSetting.StartSize];
            for (int i = 0; i < poolSetting.StartSize; i++)
            {
                pooledObjects[i] = Object.Instantiate(poolSetting.Object);
                pooledObjects[i].SetActive(false);
            }
        }
    }
}
