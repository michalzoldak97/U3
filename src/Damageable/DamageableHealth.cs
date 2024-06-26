using U3.Core;
using U3.Destructible;

namespace U3.Damageable
{
    public class DamageableHealth : Vassal<DamageableMaster>
    {
        public override void OnMasterEnabled(DamageableMaster master)
        {
            base.OnMasterEnabled(master);
            Master.EventChangeHealth += ChangeHealth;
        }

        private void OnDisable()
        {
            Master.EventChangeHealth -= ChangeHealth;
        }

        private void ChangeHealth(DamageData dmgData)
        {
            if (dmgData.RealDamage > 0f)
            {
                dmgData.RealDamage = dmgData.RealDamage < Master.Health ? dmgData.RealDamage : Master.Health;
                ObjectDamageManager.RegisterDamage(dmgData.InflictorOriginID, dmgData);
            }

            if (Master.Health > dmgData.RealDamage)
            {
                Master.Health -= dmgData.RealDamage;
                return;
            }

            Master.Health = 0f;
            Master.CallEventObjectDestruction(dmgData);
            ObjectDestructionManager.DestroyObject(gameObject);
        }

        private void Start()
        {
            Master.Health = Master.DamagableSettings.HealthSetting.InitialHealth;
        }
    }
}
