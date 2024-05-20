using U3.Core;

namespace U3.Destructible
{
    public class ProjectileImpactHandler : Vassal<DamagableMaster>
    {
        public override void OnMasterEnabled(DamagableMaster master)
        {
            base.OnMasterEnabled(master);
            Master.EventReceiveDamage += HandleDamage;
        }

        private void OnDisable()
        {
            Master.EventReceiveDamage -= HandleDamage;
        }

        private void HandleDamage(DamageData dmgData)
        {
            if (dmgData.ImpactType != DamageImpactType.ProjectileImpact ||
                dmgData.RealPenetration < Master.DamagableSettings.HealthSetting.Armor)
                return;

            dmgData.RealDamage = dmgData.RealDamage < Master.Health ? dmgData.RealDamage : Master.Health;
            Master.CallEventChangeHealth(dmgData);
        }
    }
}
