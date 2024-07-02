namespace U3.Damageable
{
    public class DamageableHitBoxEvent : DamageableHitBox
    {
        public override void OnMasterEnabled(DamageableMaster master)
        {
            base.OnMasterEnabled(master);

            FetchDamageTarget();
            Master.EventChangeHealth += SendHitBoxDamagedEvent;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            Master.EventChangeHealth -= SendHitBoxDamagedEvent;
        }

        private void SendHitBoxDamagedEvent(DamageData dmgData)
        {
            foreach (string code in Master.DamagableSettings.HitBoxSetting.HitEventCodes)
            {
                damageTarget.CallEventHitBoxDamaged(dmgData, code);
            }
        }
    }
}