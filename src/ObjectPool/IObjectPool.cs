using UnityEngine;

namespace U3.ObjectPool
{
    public interface IObjectPool
    {
        public GameObject GetObject();

        public bool AddObject(GameObject obj);
    }
}
