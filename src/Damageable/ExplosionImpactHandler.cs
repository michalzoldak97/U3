using System.Collections;
using System.Collections.Generic;
using U3.Core;
using UnityEngine;

namespace U3.Damageable
{
    public class ExplosionImpactHandler : Vassal<DamageableMaster>
    {
        private bool isExplosionDmgRegisteredInFrame;
        private readonly WaitForEndOfFrame waitForEndOfFrame = new();
        private readonly Dictionary<int, DamageData> frameExplosionDamages = new();

        public override void OnMasterEnabled(DamageableMaster master)
        {
            base.OnMasterEnabled(master);
            Master.EventReceiveExplosionDamage += RegisterExplosionDamage;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            Master.EventReceiveExplosionDamage -= RegisterExplosionDamage;
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
            if (frameExplosionDamages.ContainsKey(dmgData.InflictorOriginID))
            {
                DamageData currDmgData = frameExplosionDamages[dmgData.InflictorOriginID];
                float realDmg = GetRealDamage(dmgData.RealPenetration, dmgData.RealDamage);
                if (realDmg > currDmgData.RealDamage)
                    currDmgData.RealDamage = realDmg;
                frameExplosionDamages[dmgData.InflictorOriginID] = currDmgData;
            }
            else
            {
                dmgData.RealDamage = GetRealDamage(dmgData.RealPenetration, dmgData.RealDamage);
                frameExplosionDamages[dmgData.InflictorOriginID] = dmgData;
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
            if (!isExplosionDmgRegisteredInFrame)
            {
                StartCoroutine(ApplyExplosionDamage());
                isExplosionDmgRegisteredInFrame = true;
            }

            AddDamage(dmgData);
        }
    }
}
