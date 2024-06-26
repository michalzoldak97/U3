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

            return (pen / armor) * (1f / armor) * initDmg; // quadratic damage decrease
        }

        private void AddDamage(DamageData dmgData) // replace with max dmg for unique inflictor, increase for non-unique inflictor
        {
            float realDmg = GetRealDamage(dmgData.RealPenetration, dmgData.RealDamage);
            if (frameExplosionDamages.ContainsKey(dmgData.InflictorInstanceID))
            {
                DamageData currDmgData = frameExplosionDamages[dmgData.InflictorInstanceID];

                if (dmgData.InflictorInstanceID > 1 && realDmg > currDmgData.RealDamage)
                {
                    currDmgData.RealDamage = realDmg;
                    frameExplosionDamages[dmgData.InflictorInstanceID] = currDmgData;
                }
                else if (dmgData.InflictorInstanceID <= 1)
                {
                    currDmgData.RealDamage += realDmg;
                    frameExplosionDamages[dmgData.InflictorInstanceID] = currDmgData;
                }
            }
            else
            {
                dmgData.RealDamage = realDmg;
                frameExplosionDamages[dmgData.InflictorInstanceID] = dmgData;
            }
        }

        private void ApplyFrameDamage()
        {
            float dmgMultiplayer = Master.DamagableSettings.HealthSetting.ExplosionDamageMultiplayer;
            foreach (DamageData dmgData in frameExplosionDamages.Values)
            {
                DamageData dmgToApply = dmgData;
                dmgToApply.RealDamage = dmgData.RealDamage * dmgMultiplayer;
                Debug.Log($"{gameObject.name} Applying explosion damage: inflictor id {dmgData.InflictorOriginID}, inflicotr instance {dmgData.InflictorInstanceID}, init dmg {dmgData.RealDamage} init pen {dmgData.RealPenetration}");
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
            Debug.Log($"{gameObject.name} Registering explosion damage: inflictor id {dmgData.InflictorOriginID}, inflicotr instance {dmgData.InflictorInstanceID}, init dmg {dmgData.RealDamage} init pen {dmgData.RealPenetration}");
            AddDamage(dmgData);
        }
    }
}
