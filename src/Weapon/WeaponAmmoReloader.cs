﻿using System.Collections;
using U3.Core;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponAmmoReloader : Vassal<WeaponMaster>
    {
        public override void OnMasterEnabled(WeaponMaster master)
        {
            base.OnMasterEnabled(master);

            Master.EventReloadCalled += OnReloadCalled;

            Master.EventInputInterrupted += OnReloadInterrupted;
        }

        private void OnDisable()
        {

            Master.EventReloadCalled -= OnReloadCalled;
            Master.EventInputInterrupted -= OnReloadInterrupted;

            Master.IsReloading = false;
        }

        private bool IsReloadingBlocked()
        {
            return (Master.IsShooting || Master.IsShootingBurst || Master.IsReloading);
        }

        private IEnumerator Reload(IAmmoStore ammoStore)
        {
            Master.IsReloading = true;
            Master.CallEventReloadStarted();

            yield return new WaitForSeconds(Master.WeaponSettings.ReloadDurationSeconds);

            if (!Master.IsReloading)
                yield break;

            Master.AmmoInMag += ammoStore.RetrieveAmmo(Master.WeaponSettings.AmmoSettings.MagazineSize - Master.AmmoInMag, Master.AmmoCode);


            Master.IsReloading = false;
            Master.CallEventReloadFinnished();
        }

        private void OnReloadCalled(IAmmoStore ammoStore)
        {
            if (IsReloadingBlocked())
                return;

            StartCoroutine(Reload(ammoStore));
        }

        private void OnReloadInterrupted()
        {
            Master.IsReloading = false;
        }
    }
}
