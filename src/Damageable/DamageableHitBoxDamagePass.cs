using U3.Core;
using U3.Log;

namespace U3.Damageable
{
    public class DamageableHitBoxDamagePass : Vassal<DamageableMaster>
    {
        private IDamageReciever damageTarget;

        private void FetchDamageTarget()
        {
            if (damageTarget != null)
                return;

            if (transform.root.TryGetComponent(out IDamageReciever dmgTarget))
            {
                damageTarget = dmgTarget;
            }
            else
            {
                GameLogger.Log(new GameLog(LogType.Error, $"Required IDamageReciever not found on object {transform.root.gameObject.name}"));
            }
        }

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
