using System.Collections.Generic;
using U3.Destructible;
using UnityEngine;

namespace U3.ObjectPool
{
    public class ObjectPoolsManager : MonoBehaviour
    {
        [SerializeField] private ObjectPoolSettings poolSettings;

        public static ObjectPoolsManager Instance;

        private readonly Dictionary<string, IObjectPool<DamageInflictor>> objectPools = new();

        public PooledObject<DamageInflictor> GetObject(string code) => objectPools[code].GetObject();

        public bool AddObject(string code, PooledObject<DamageInflictor> obj) => objectPools[code].AddObject(obj);

        private void InitializePools()
        {
            foreach (ObjectPoolSetting poolSetting in poolSettings.ObjectPools)
            {
                switch (poolSetting.Type)
                {
                    case ObjectPoolType.DamageInflictor:
                        objectPools[poolSetting.Code] = ObjectPoolFactory.New<DamageInflictor>(poolSetting);
                        break;
                    default:
                        break;
                }
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
