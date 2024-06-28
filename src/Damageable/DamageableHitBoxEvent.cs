using U3.Core;


namespace U3.Damageable
{
    public class DamageableHitBoxEvent : DamageableHitBox
    {
        public override void OnMasterEnabled(DamageableMaster master)
        {
            base.OnMasterEnabled(master);

            FetchDamageTarget();
            Master.EventChangeHealth += SendHitBoxEvent;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            Master.EventChangeHealth -= SendHitBoxEvent;
        }

        private void SendHitBoxDamagedEvent(DamageData dmgData)
        {
            foreach (string code in Master.DamagableSettings.HitEventCodes)
            {
                damageTarget.CallEventHitBoxDamaged(dmgData, code);
            }
        }
    }
}