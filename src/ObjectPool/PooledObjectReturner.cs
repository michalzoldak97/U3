﻿using U3.Destructible;
using UnityEngine;

namespace U3.ObjectPool
{
    public class PooledObjectReturner : MonoBehaviour
    {
        public string PoolCode { get; set; }

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
