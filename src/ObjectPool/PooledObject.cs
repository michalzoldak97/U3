using UnityEngine;

namespace U3.ObjectPool
{
    public class PooledObject
    {
        public Transform ObjTransform { get; }
        public Rigidbody ObjRigidbody { get; }
        public GameObject Obj { get; }

        public PooledObject(GameObject obj)
        {
            Obj = obj;
            ObjTransform = obj.transform;
            ObjRigidbody = obj.GetComponent<Rigidbody>();
        }
    }
}
