using UnityEngine;

namespace U3.Destructible
{
    public class DamageInflictor : MonoBehaviour
    {
        [SerializeField] protected DamageInflictorSettings dmgSettings;

        private int instanceID;

        protected void InflictDamage(Transform toDmg,float realDamage, float realPenetration)
        {
            DamageData dmgDataToPass = ObjectDamageManager.GetDamageInflictorData(instanceID);
            dmgDataToPass.RealDamage = realDamage;
            dmgDataToPass.RealPenetration = realPenetration;

            ObjectDamageManager.InflictDamage(toDmg, dmgDataToPass);
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
            instanceID = gameObject.GetInstanceID();
            ObjectDamageManager.RegisterDamageInflictor(instanceID, CreateDmgData());
        }
    }
}
