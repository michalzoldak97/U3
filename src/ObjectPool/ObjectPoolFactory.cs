namespace U3.ObjectPool
{
    internal static class ObjectPoolFactory
    {
        public static IObjectPool New(ObjectPoolSetting poolSetting)
        {
            if (poolSetting.InstantiatingPoolSetting == null)
                return new RetrievingObjectPool(poolSetting);
            else 
                return new InstantiatingObjectPool(poolSetting);
        }
    }
}
