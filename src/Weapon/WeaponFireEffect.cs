using System.Collections;
using U3.Core;
using U3.Item;
using U3.Log;
using UnityEngine;
using UnityEngine.VFX;

namespace U3.Weapon
{
    public class WeaponFireEffect : Vassal<WeaponMaster>
    {
        private bool shouldEffectBeEnabled, isActivityCheckInProgress;
        private readonly WaitForSeconds nextCheckDelay = new (1f);
        private readonly WaitForSeconds nextEnabledCheckDelay = new (1.5f);
        private VisualEffect effect;

        public override void OnMasterEnabled(WeaponMaster master)
        {
            base.OnMasterEnabled(master);

            Master.EventWeaponFired += OnShoot;

            if (effect == null)
                effect = GetComponentInChildren<VisualEffect>();

            if (effect == null)
                GameLogger.Log(new GameLog(Log.LogType.Error, $"Required VisualEffect not found on object {gameObject.name}"));

            shouldEffectBeEnabled = false;
            effect.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Master.EventWeaponFired -= OnShoot;

            shouldEffectBeEnabled = false;
            isActivityCheckInProgress = false;
            effect.gameObject.SetActive(false);
            StopAllCoroutines();
        }

        private IEnumerator DisableEffect()
        {
            while (isActivityCheckInProgress)
            {
                yield return nextEnabledCheckDelay;
            }
            shouldEffectBeEnabled = false;
            effect.gameObject.SetActive(false);
        }

        private IEnumerator DisableActivityCheck()
        {
            yield return nextCheckDelay;
            isActivityCheckInProgress = false;
        }

        private void ManageEffectActivityFlags()
        {
            if (isActivityCheckInProgress)
                return;

            isActivityCheckInProgress = true;
            StartCoroutine(DisableActivityCheck());
            StartCoroutine(DisableEffect());
        }

        private void OnShoot(FireInputOrigin _)
        {
            ManageEffectActivityFlags();
            if (!shouldEffectBeEnabled)
            {
                effect.gameObject.SetActive(true);
                shouldEffectBeEnabled = true;
            }

            effect.Play();
        }
    }
}
