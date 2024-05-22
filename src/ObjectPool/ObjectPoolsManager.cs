using System.Collections.Generic;
using UnityEngine;

namespace U3.ObjectPool
{
    public class ObjectPoolsManager<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private ObjectPoolSettings poolSettings;

        public static ObjectPoolsManager<T> Instance;

        private readonly Dictionary<string, IObjectPool<T>> objectPools = new();

        public PooledObject<T> GetObject(string code) => objectPools[code].GetObject();

        public bool AddObject(string code, PooledObject<T> obj) => objectPools[code].AddObject(obj);

        private void InitializePools()
        {
            foreach (ObjectPoolSetting poolSetting in poolSettings.ObjectPools)
            {
                objectPools[poolSetting.Code] = ObjectPoolFactory.New<T>(poolSetting);
            }
        }

        // TODO: (API) wipe out pools and reconstruct for the specific scene situation (guns available, bots weapons...)

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            InitializePools();
        }
    }
}
