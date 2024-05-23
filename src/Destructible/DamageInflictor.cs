using U3.ObjectPool;
using UnityEngine;

namespace U3.Destructible
{
    public class DamageInflictor : MonoBehaviour, IPoolReturnable<DamageInflictor>
    {
        public string PoolCode { get; set; }

        [SerializeField] protected DamageInflictorSettings dmgSettings;
        protected DamageData m_DmgData;
        private PooledObject<DamageInflictor> m_PooledObject;

        public PooledObject<DamageInflictor> GetPooledObject() => m_PooledObject;
        public void SetPooledObject(PooledObject<DamageInflictor> toSet)
        {
            m_PooledObject = toSet;
        }

        public void SetInflictorData(int inflictorID, LayerMask layersToHit, LayerMask layersToDamage)
        {
            m_DmgData.InflictorID = inflictorID;
            m_DmgData.LayersToHit = layersToHit;
            m_DmgData.LayersToDamage = layersToDamage;
        }

        protected void InflictDamage(Transform toDmg,float realDamage, float realPenetration)
        {
            m_DmgData.RealDamage = realDamage;
            m_DmgData.RealPenetration = realPenetration;

            ObjectDamageManager.InflictDamage(toDmg, m_DmgData);
        }

        private DamageData CreateDmgData()
        {
            return new DamageData()
            {
                ImpactType = dmgSettings.ImpactType,
                ElementType = dmgSettings.ElementType,
                RealDamage = dmgSettings.Damage
            };
        }

        protected void SpawnHitEffect(Collision col)
        {
            // CallGlobalEffectSpawner()
        }

        private void Awake()
        {
            m_DmgData = CreateDmgData();
        }

        public void ReturnToPool()
        {
            bool isObjAccepted = ObjectPoolsManager.Instance.AddObject(PoolCode, m_PooledObject);

            if (!isObjAccepted)
                ObjectDestructionManager.DestroyObject(gameObject);
        }
    }
}
