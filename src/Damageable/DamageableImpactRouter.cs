using U3.Core;
using UnityEngine;

namespace U3.Damageable
{
    public class DamageableImpactRouter : Vassal<DamageableMaster>
    {
        public override void OnMasterEnabled(DamageableMaster master)
        {
            base.OnMasterEnabled(master);
            Master.EventReceiveDamage += RouteDamage;
        }

        private void OnDisable()
        {
            Master.EventReceiveDamage -= RouteDamage;
        }

        private void RouteDamage(DamageData dmgData)
        {
            switch (dmgData.ImpactType)
            {
                case DamageImpactType.ProjectileImpact:
                    Master.EventReceiveProjectileDamage(dmgData);
                    break;
                case DamageImpactType.ExplosionImpact:
                    Master.EventReceiveExplosionDamage(dmgData);
                    break;
                default:
                    break;
            }
        }
    }
}