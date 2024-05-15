using U3.Log;
using UnityEngine;

namespace U3.ObjectPool
{
    public class PooledObject
    {
        public bool IsFromPool { get; }
        public int ObjInstanceID { get; }
        public int PoolIndex { get; }
        public Transform ObjTransform { get; }
        public Rigidbody ObjRigidbody { get; }
        public GameObject Obj { get; }

        public PooledObject(GameObject obj, string poolCode, int poolIndex, bool isFromPool)
        {
            IsFromPool = isFromPool;
            ObjInstanceID = obj.GetInstanceID();
            PoolIndex = poolIndex;
            Obj = obj;
            ObjTransform = obj.transform;
            ObjRigidbody = obj.GetComponent<Rigidbody>();

            if (obj.TryGetComponent(out ReturnToObjectPool returnToPool))
            {
                returnToPool.PoolCode = poolCode;
                returnToPool.PooledObject = this;
            }
            else
            {
                GameLogger.Log(new GameLog(Log.LogType.Warning, $"pooled object {obj.name} is missing return to pool"));
            }
        }
    }
}
