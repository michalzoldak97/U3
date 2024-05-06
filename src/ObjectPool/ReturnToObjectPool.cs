using U3.Destructible;
using UnityEngine;

namespace U3.ObjectPool
{
    public class ReturnToObjectPool : MonoBehaviour
    {
        public string PoolCode { get; set; }
        public PooledObject PooledObject { get; set; }

        protected void ReturnToPool()
        {
            bool isObjAccepted = ObjectPoolsManager.Instance.AddObject(PoolCode, PooledObject);

            if (!isObjAccepted)
                ObjectDestructionManager.DestroyObject(gameObject);
        }
    }
}
