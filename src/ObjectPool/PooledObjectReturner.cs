using U3.Destructible;
using UnityEngine;

namespace U3.ObjectPool
{
    public class PooledObjectReturner : MonoBehaviour
    {
        public bool IsFromPool { get; private set; }
        public string PoolCode { get; private set; }

        public void SetPoolCode(string codeToSet)
        {
            PoolCode = codeToSet;
            IsFromPool = true;
        }

        public virtual void SetPooledOject<T>(PooledObject<T> obj) where T : Component { }

        protected virtual bool TryReturnToPool() { return false; }

        public virtual void ReturnToPool<T>() where T : Component
        {
            bool isObjAccepted = TryReturnToPool();

            if (!isObjAccepted)
                ObjectDestructionManager.DestroyObject(gameObject);
        }
    }
}
