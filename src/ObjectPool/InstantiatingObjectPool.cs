using System.Collections.Generic;
using UnityEngine;

namespace U3.ObjectPool
{
    internal class InstantiatingObjectPool<T> : IObjectPool<T> where T : Component
    {
        private bool isCountLimitReached;
        private int currentCount;
        private readonly int expandCount, expandCountLimit;
        private readonly List<int> availableObjIndexes;
        private readonly List<PooledObject<T>> pooledObjects;
        private readonly ObjectPoolSetting m_poolSetting;

        private void AddNewObject(int index)
        {
            PooledObject<T> newObj = PooledObjectFactory.New<T>(m_poolSetting, index);
            pooledObjects.Add(newObj);
            availableObjIndexes.Add(index);
            newObj.Obj.SetActive(false);
            currentCount++;
        }

        private PooledObject<T> GetNext()
        {
            int indexToReturn = availableObjIndexes[0];
            availableObjIndexes.Remove(indexToReturn);
            return pooledObjects[indexToReturn];
        }

        private void ExpandPoolByCount()
        {
            int startCount = currentCount;
            for (int i = startCount; i < (startCount + expandCount); i++)
            {
                AddNewObject(i);
            }
        }

        private PooledObject<T> OnPoolSaturated()
        {
            if (isCountLimitReached)
                return PooledObjectFactory.New<T>(m_poolSetting, isFromPool: false);

            if (currentCount + expandCount >= expandCountLimit)
            {
                isCountLimitReached = true;
                return PooledObjectFactory.New<T>(m_poolSetting, isFromPool: false);
            }

            ExpandPoolByCount();
            return GetNext();
        }

        public PooledObject<T> GetObject()
        {
            if (availableObjIndexes.Count < 1)
                return OnPoolSaturated();

            return GetNext();
        }

        public bool AddObject(PooledObject<T> obj)
        {
            if (!obj.IsFromPool)
                return false;

            availableObjIndexes.Add(obj.PoolIndex);
            obj.Obj.SetActive(false);
            return true;
        }

        public InstantiatingObjectPool(ObjectPoolSetting poolSetting)
        {
            expandCount = poolSetting.InstantiatingPoolSetting.ExpandCount;
            expandCountLimit = poolSetting.InstantiatingPoolSetting.ExpandCountLimit;
            m_poolSetting = poolSetting;

            int maxCount = poolSetting.StartSize + poolSetting.InstantiatingPoolSetting.OverheadBufferCount;
            availableObjIndexes = new List<int>(maxCount);
            pooledObjects = new (maxCount);

            for (int i = 0; i < poolSetting.StartSize; i++)
            {
                AddNewObject(i);
            }
        }
    }
}
