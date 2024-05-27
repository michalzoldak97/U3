using UnityEngine;

namespace U3.Destructible
{
    public class DamageInflictor : MonoBehaviour
    {
        [SerializeField] protected DamageInflictorSettings dmgSettings;
        protected DamageData m_DmgData;
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
    }
}
