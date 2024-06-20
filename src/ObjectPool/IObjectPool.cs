using UnityEngine;

namespace U3.ObjectPool
{
    internal interface IObjectPool<T> where T : Component
    {
        public PooledObject<T> GetObject();

        public bool AddObject(PooledObject<T> obj);
    }
}
