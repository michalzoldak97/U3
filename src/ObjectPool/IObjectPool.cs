namespace U3.ObjectPool
{
    internal interface IObjectPool
    {
        public PooledObject GetObject();

        public bool AddObject(PooledObject obj);
    }
}
