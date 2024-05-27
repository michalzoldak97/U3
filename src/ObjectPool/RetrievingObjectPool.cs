using UnityEngine;

namespace U3.ObjectPool
{
    internal class RetrievingObjectPool<T> : IObjectPool<T> where T : Component
    {
        private int curentIndex;
        private readonly int maxIndex;
        private readonly PooledObject<T>[] pooledObjects;

        private PooledObject<T> RetrieveObject()
        {
            curentIndex = 0;
            pooledObjects[0].Obj.SetActive(false);
            return pooledObjects[0];
        }

        public PooledObject<T> GetObject()
        {
            if (curentIndex >= maxIndex)
                return RetrieveObject();

            PooledObject<T> obj = pooledObjects[curentIndex];
            curentIndex++;

            pooledObjects[0].Obj.SetActive(false);
            return obj;
        }

        public bool AddObject(PooledObject<T> _) => true;

        public RetrievingObjectPool(ObjectPoolSetting poolSetting)
        {
            pooledObjects = new PooledObject<T>[poolSetting.StartSize];
            for (int i = 0; i < poolSetting.StartSize; i++)
            {
                pooledObjects[i] = PooledObjectFactory.New<T>(poolSetting);
                pooledObjects[0].Obj.SetActive(false);
            }

            maxIndex = poolSetting.StartSize - 1;
        }
    }
}
