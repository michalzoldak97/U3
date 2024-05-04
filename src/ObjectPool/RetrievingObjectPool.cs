namespace U3.ObjectPool
{
    internal class RetrievingObjectPool : IObjectPool
    {
        private int curentIndex;
        private readonly int maxIndex;
        private readonly PooledObject[] pooledObjects;

        private PooledObject RetrieveObject()
        {
            curentIndex = 0;
            pooledObjects[0].Obj.SetActive(false);
            return pooledObjects[0];
        }

        public PooledObject GetObject()
        {
            if (curentIndex >= maxIndex)
                return RetrieveObject();

            PooledObject obj = pooledObjects[curentIndex];
            curentIndex++;

            obj.Obj.SetActive(false);
            return obj;
        }

        public bool AddObject(PooledObject _) => true;

        public RetrievingObjectPool(ObjectPoolSetting poolSetting)
        {
            pooledObjects = new PooledObject[poolSetting.StartSize];
            for (int i = 0; i < poolSetting.StartSize; i++)
            {
                pooledObjects[i] = PooledObjectFactory.New(poolSetting);
                pooledObjects[i].Obj.SetActive(false);
            }

            maxIndex = poolSetting.StartSize - 1;
        }
    }
}
