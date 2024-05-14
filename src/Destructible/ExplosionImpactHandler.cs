using System.Collections;
using System.Collections.Generic;
using U3.Core;
using UnityEngine;

namespace U3.Destructible
{
    public class ExplosionImpactHandler : Vassal<DamagableMaster>
    {
        private bool isExplosionDmgRegisteredInFrame;
        private readonly WaitForEndOfFrame waitForEndOfFrame = new();
        private readonly Dictionary<int, DamageData> frameExplosionDamages = new();

        public override void OnMasterEnabled(DamagableMaster master)
        {
            base.OnMasterEnabled(master);
            Master.EventReceiveDamage += RegisterExplosionDamage;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            Master.EventReceiveDamage -= RegisterExplosionDamage;
        }

        private int GetMaxDamage()
        {
            int maxDmgID = 0;
            float maxDmg = 0;
            foreach (KeyValuePair<int, DamageData> dmgData in frameExplosionDamages)
            {
                if (dmgData.Value.Damage > maxDmg)
                {
                    maxDmgID = dmgData.Value.InflictorID;
                    maxDmg = dmgData.Value.Damage;
                }
            }

            return maxDmgID;
        }

        private IEnumerator ApplyExplosionDamage()
        {
            yield return waitForEndOfFrame;

            DamageData dmgToApply = frameExplosionDamages[GetMaxDamage()];
            dmgToApply.RealDamage = dmgToApply.Damage < Master.Health ? dmgToApply.Damage : Master.Health;
            Master.CallEventChangeHealth(dmgToApply);

            frameExplosionDamages.Clear();
            isExplosionDmgRegisteredInFrame = false;
        }

        private void AddDamage(DamageData dmgData)
        {
            if (frameExplosionDamages.ContainsKey(dmgData.InflictorID))
            {
                DamageData currDmgData = frameExplosionDamages[dmgData.InflictorID];
                currDmgData.Damage += dmgData.Damage;
                frameExplosionDamages[dmgData.InflictorID] = currDmgData;
            }
            else
                frameExplosionDamages[dmgData.InflictorID] = dmgData;
        }

        private void RegisterExplosionDamage(DamageData dmgData)
        {
            if (dmgData.ImpactType != DamageImpactType.ExplosionImpact ||
                dmgData.Penetration < Master.DamagableSettings.HealthSetting.Armor)
                return;

            if (!isExplosionDmgRegisteredInFrame)
            {
                StartCoroutine(ApplyExplosionDamage());
                isExplosionDmgRegisteredInFrame = true;
            }

            AddDamage(dmgData);
        }
    }
}
