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
                dmgData.Penetration < Master.DamagableSettings.HealthSetting.Armor)
                return;

            dmgData.RealDamage = dmgData.Damage < Master.Health ? dmgData.Damage : Master.Health;
            Master.CallEventChangeHealth(dmgData);
        }
    }
}
