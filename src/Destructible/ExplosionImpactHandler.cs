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

        private float GetRealDamage(float pen, float initDmg)
        {
            float armor = Master.DamagableSettings.HealthSetting.Armor;
            if (pen >= armor)
                return initDmg;

            return (pen / armor) * (1f / armor); // quadratic damage decrease
        }

        private void AddDamage(DamageData dmgData)
        {
            if (frameExplosionDamages.ContainsKey(dmgData.InflictorID))
            {
                DamageData currDmgData = frameExplosionDamages[dmgData.InflictorID];
                float realDmg = GetRealDamage(dmgData.RealPenetration, dmgData.RealDamage);
                if (realDmg > currDmgData.RealDamage)
                    currDmgData.RealDamage = realDmg;
                frameExplosionDamages[dmgData.InflictorID] = currDmgData;
            }
            else
            {
                dmgData.RealDamage = GetRealDamage(dmgData.RealPenetration, dmgData.RealDamage);
                frameExplosionDamages[dmgData.InflictorID] = dmgData;
            }
        }

        private void ApplyFrameDamage()
        {
            foreach (DamageData dmgData in frameExplosionDamages.Values)
            {
                DamageData dmgToApply = dmgData;
                dmgToApply.RealDamage = dmgData.RealDamage < Master.Health ? dmgData.RealDamage : Master.Health;
                Master.CallEventChangeHealth(dmgToApply);
            }
        }

        private IEnumerator ApplyExplosionDamage()
        {
            yield return waitForEndOfFrame;

            ApplyFrameDamage();

            frameExplosionDamages.Clear();
            isExplosionDmgRegisteredInFrame = false;
        }

        private void RegisterExplosionDamage(DamageData dmgData)
        {
            if (dmgData.ImpactType != DamageImpactType.ExplosionImpact)
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
