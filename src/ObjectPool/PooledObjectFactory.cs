using UnityEngine;

namespace U3.ObjectPool
{
    internal static class PooledObjectFactory
    {
        public static PooledObject<T> New<T>(ObjectPoolSetting poolSetting, int poolIndex = -1, bool isFromPool = true) where T : Component
        {
            GameObject obj = Object.Instantiate(poolSetting.Object);
            return new PooledObject<T>(obj, poolSetting.Code, poolIndex, isFromPool);
        }
    }
}
