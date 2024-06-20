using U3.Core;
using UnityEngine;

namespace U3.Damageable
{
    public class ProjectileImpactHandler : Vassal<DamageableMaster>
    {
        public override void OnMasterEnabled(DamageableMaster master)
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

            Debug.Log($"Applying projectile damage");

            dmgData.RealDamage = dmgData.RealDamage < Master.Health ? dmgData.RealDamage : Master.Health;
            Master.CallEventChangeHealth(dmgData);
        }
    }
}
