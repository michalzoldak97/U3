using UnityEngine;

namespace U3.ObjectPool
{
    internal static class ObjectPoolFactory
    {
        public static IObjectPool<T> New<T>(ObjectPoolSetting poolSetting) where T : Component
        {
            if (poolSetting.InstantiatingPoolSetting.AllowExpand)
                return new InstantiatingObjectPool<T>(poolSetting);
            else 
                return new RetrievingObjectPool<T>(poolSetting);
        }
    }
}
