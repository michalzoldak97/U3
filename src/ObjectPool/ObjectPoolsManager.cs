using System.Collections.Generic;
using U3.Damageable;
using U3.Weapon.Effect;
using UnityEngine;

namespace U3.ObjectPool
{
    public class ObjectPoolsManager : MonoBehaviour
    {
        [SerializeField] private ObjectPoolSettings poolSettings;

        public static ObjectPoolsManager Instance;

        private readonly Dictionary<string, IObjectPool<DamageInflictor>> dmgInflictorPools = new();
        private readonly Dictionary<string, IObjectPool<HitEffect>> effectPools = new();
        private readonly Dictionary<string, IObjectPool<DamageText>> damageTextPools = new();


        public PooledObject<DamageInflictor> GetDamageInflictor(string code) => dmgInflictorPools[code].GetObject();
        public PooledObject<HitEffect> GetEffect(string code) => effectPools[code].GetObject();
        public PooledObject<DamageText> GetDamageText(string code) => damageTextPools[code].GetObject();

        public bool AddDamageInflictor(string code, PooledObject<DamageInflictor> obj) => dmgInflictorPools[code].AddObject(obj);
        public bool AddEffect(string code, PooledObject<HitEffect> obj) => effectPools[code].AddObject(obj);
        public bool AddDamageText(string code, PooledObject<DamageText> obj) => damageTextPools[code].AddObject(obj);

        private void InitializePools()
        {
            foreach (ObjectPoolSetting poolSetting in poolSettings.ObjectPools)
            {
                switch (poolSetting.Type)
                {
                    case ObjectPoolType.DamageInflictor:
                        dmgInflictorPools[poolSetting.Code] = ObjectPoolFactory.New<DamageInflictor>(poolSetting);
                        break;
                    case ObjectPoolType.Effect:
                        effectPools[poolSetting.Code] = ObjectPoolFactory.New<HitEffect>(poolSetting);
                        break;
                    case ObjectPoolType.DamageText:
                        damageTextPools[poolSetting.Code] = ObjectPoolFactory.New<DamageText>(poolSetting);
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
