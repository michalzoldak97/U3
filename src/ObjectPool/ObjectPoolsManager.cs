using System.Collections.Generic;
using UnityEngine;

namespace U3.ObjectPool
{
    public class ObjectPoolsManager : MonoBehaviour
    {
        [SerializeField] private ObjectPoolSettings poolSettings;

        public static ObjectPoolsManager Instance;

        private readonly Dictionary<string, IObjectPool> objectPools = new();

        public PooledObject GetObject(string code) => objectPools[code].GetObject();

        public bool AddObject(string code, PooledObject obj) => objectPools[code].AddObject(obj);

        private void InitializePools()
        {
            foreach (ObjectPoolSetting poolSetting in poolSettings.ObjectPools)
            {
                objectPools[poolSetting.Code] = ObjectPoolFactory.New(poolSetting);
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
