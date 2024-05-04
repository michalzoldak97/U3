namespace U3.ObjectPool
{
    internal static class ObjectPoolFactory
    {
        public static IObjectPool New(ObjectPoolSetting poolSetting)
        {
            if (poolSetting.InstantiatingPoolSetting.AllowExpand)
                return new InstantiatingObjectPool(poolSetting);
            else 
                return new RetrievingObjectPool(poolSetting);
        }
    }
}
