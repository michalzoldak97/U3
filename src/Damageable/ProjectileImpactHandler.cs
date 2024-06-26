using U3.Core;
using UnityEngine;

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
            Debug.Log($"{gameObject.name} Registering projectile damage: inflictor id {dmgData.InflictorOriginID}, inflicotr instance {dmgData.InflictorInstanceID}, init dmg {dmgData.RealDamage} init pen {dmgData.RealPenetration}");
            if (dmgData.RealPenetration < Master.DamagableSettings.HealthSetting.Armor)
                return;

            dmgData.RealDamage = dmgData.RealDamage * Master.DamagableSettings.HealthSetting.ProjectileDamageMultiplayer;
            Debug.Log($"{gameObject.name} Applying projectile damage: inflictor id {dmgData.InflictorOriginID}, inflicotr instance {dmgData.InflictorInstanceID}, init dmg {dmgData.RealDamage} init pen {dmgData.RealPenetration}");
            Master.CallEventChangeHealth(dmgData);
        }
    }
}
