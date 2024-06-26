﻿namespace U3.Damageable
{
    public class DamageableHitBoxDamagePass : DamageableHitBox
    {
        public override void OnMasterEnabled(DamageableMaster master)
        {
            base.OnMasterEnabled(master);

            FetchDamageTarget();
            Master.EventChangeHealth += PassDamage;
        }

        private void OnDisable()
        {
            Master.EventChangeHealth -= PassDamage;
        }

        private void PassDamage(DamageData dmgData)
        {
            dmgData.RealPenetration = dmgData.RealPenetration - Master.DamagableSettings.HitBoxSetting.PenetrationReduction;
            if (dmgData.RealPenetration < 1)
                return;

            dmgData.RealDamage = dmgData.RealDamage * Master.DamagableSettings.HitBoxSetting.DamageMultiplayer;
            damageTarget.CallEventReceiveDamage(dmgData);
        }
    }
}
