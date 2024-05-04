using System.Collections.Generic;

namespace U3.ObjectPool
{
    internal class InstantiatingObjectPool : IObjectPool
    {
        private int curentIndex;
        private int maxIndex;

        private readonly List<PooledObject> pooledObjects;
        private readonly ObjectPoolSetting m_poolSetting;

        private PooledObject GetNext()
        {
            PooledObject obj = pooledObjects[curentIndex];
            curentIndex++;

            obj.Obj.SetActive(false);
            return obj;
        }

        private void ExpandPoolByCount()
        {
            for (int i = 0; i < m_poolSetting.InstantiatingPoolSetting.ExpandCount; i++)
            {
                PooledObject newObj = PooledObjectFactory.New(m_poolSetting);
                pooledObjects.Add(newObj);
                newObj.Obj.SetActive(false);
                maxIndex++;
            }
        }

        private PooledObject OnPoolSaturated()
        {
            if (maxIndex >= m_poolSetting.InstantiatingPoolSetting.ExpandSizeLimit)
                return PooledObjectFactory.New(m_poolSetting);

            ExpandPoolByCount();
            return GetNext();
        }

        public PooledObject GetObject()
        {
            if (curentIndex >= maxIndex)
                return OnPoolSaturated();

            return GetNext();
        }

        public bool AddObject(PooledObject obj)
        {
            return false;
            if (maxIndex >= m_poolSetting.InstantiatingPoolSetting.ExpandSizeLimit)
                return false;
            // TODO: fast lookup
            /*
             * if (structDictionary.TryGetValue(someValue, out var result))
            {
                // 'result' contains the Struct where Two is true
            }
             */

        }

        public InstantiatingObjectPool(ObjectPoolSetting poolSetting)
        {
            pooledObjects = new (poolSetting.StartSize + poolSetting.InstantiatingPoolSetting.OverheadBufferCount);
            for (int i = 0; i < poolSetting.StartSize; i++)
            {
                pooledObjects[i] = PooledObjectFactory.New(poolSetting);
                pooledObjects[i].Obj.SetActive(false);
            }

            maxIndex = poolSetting.StartSize - 1;
            m_poolSetting = poolSetting;
        }
    }
}
