using UnityEngine;

namespace U3.ObjectPool
{
    public interface IPoolReturnable<T> where T : Component
    {
        public string PoolCode { get; set; }
        public PooledObject<T> GetPooledObject();

        public void SetPooledObject(PooledObject<T> obj);

        public void ReturnToPool();
    }
}
