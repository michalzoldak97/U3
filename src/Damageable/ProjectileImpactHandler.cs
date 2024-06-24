using U3.Core;

namespace U3.Damageable
{
    public class ProjectileImpactHandler : Vassal<DamageableMaster>
    {
        public override void OnMasterEnabled(DamageableMaster master)
        {
            base.OnMasterEnabled(master);
            Master.EventReceiveProjectileDamage += HandleDamage;
        }

        private void OnDisable()
        {
            Master.EventReceiveProjectileDamage -= HandleDamage;
        }

        private void HandleDamage(DamageData dmgData)
        {
            if (dmgData.RealPenetration < Master.DamagableSettings.HealthSetting.Armor)
                return;

            dmgData.RealDamage = dmgData.RealDamage < Master.Health ? dmgData.RealDamage : Master.Health;
            Master.CallEventChangeHealth(dmgData);
        }
    }
}
