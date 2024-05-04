using UnityEngine;

namespace U3.ObjectPool
{
    internal static class PooledObjectFactory
    {
        public static PooledObject New(ObjectPoolSetting poolSetting)
        {
            GameObject obj = Object.Instantiate(poolSetting.Object);
            return new PooledObject(obj);
        }
    }
}
