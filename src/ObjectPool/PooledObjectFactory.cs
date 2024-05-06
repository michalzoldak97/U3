using UnityEngine;

namespace U3.ObjectPool
{
    internal static class PooledObjectFactory
    {
        public static PooledObject New(ObjectPoolSetting poolSetting, int poolIndex = -1, bool isFromPool = true)
        {
            GameObject obj = Object.Instantiate(poolSetting.Object);
            return new PooledObject(obj, poolSetting.Code, poolIndex, isFromPool);
        }
    }
}
