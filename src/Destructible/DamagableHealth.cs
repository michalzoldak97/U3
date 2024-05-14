using U3.Core;

namespace U3.Destructible
{
    public class DamagableHealth : Vassal<DamagableMaster>
    {
        public override void OnMasterEnabled(DamagableMaster master)
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
                ObjectDamageManager.RegisterDamage(dmgData.InflictorID, dmgData);

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
