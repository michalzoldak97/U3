using System.Collections;
using U3.Core;
using U3.Item;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponFire : Vassal<WeaponMaster>
    {
        private WaitForSeconds waitForNextAutoShoot;
        private WaitForSeconds waitForNextBurstShoot;

        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            Master.EventFireDownCalled += OnFireStart;
            Master.EventFireUpCalled += OnFireStop;

            Master.EventInputInterrupted += OnInputInterrupted;
        }

        private void OnDisable()
        {
            Master.EventFireDownCalled -= OnFireStart;
            Master.EventFireUpCalled -= OnFireStop;

            Master.EventInputInterrupted -= OnInputInterrupted;

            Master.IsShooting = false;
            Master.IsShootingBurst = false;
        }

        private bool IsShootingBlocked()
        {
            if (Master.IsShooting || Master.IsShootingBurst || Master.IsReloading)
                return true;

            if (!Master.IsLoaded)
            {
                Master.CallEventFireCalledOnUnloaded();
                return true;
            }

            return false;
        }

        private void ShootSingle(FireInputOrigin inputOrigin)
        {
            Master.IsShooting = true;
            Master.CallEventFire(inputOrigin);
        }

        private IEnumerator ShootBurst(FireInputOrigin inputOrigin)
        {
            Master.IsShooting = true;
            Master.IsShootingBurst = true;

            int shoots = Master.WeaponSettings.GunSettings.ShootsInBurst;

            for (int i = 0; i < shoots; i++)
            {
                if (Master.IsShootingBurst && Master.IsLoaded)
                    Master.CallEventFire(inputOrigin);
                else if (!Master.IsLoaded)
                {
                    Master.CallEventFireCalledOnUnloaded();
                    break;
                }
                else
                    break;
                yield return waitForNextBurstShoot;
            }

            Master.IsShootingBurst = false;
            Master.IsShooting = false;
        }

        private IEnumerator ShootAuto(FireInputOrigin inputOrigin)
        {
            Master.IsShooting = true;
            while (Master.IsShooting)
            {
                if (!Master.IsLoaded)
                {
                    Master.CallEventFireCalledOnUnloaded();
                    break;
                }

                Master.CallEventFire(inputOrigin);
                yield return waitForNextAutoShoot;
            }
        }

        private void OnFireStart(FireInputOrigin inputOrigin)
        {
            if (IsShootingBlocked())
                return;

            switch (Master.FireMode)
            {
                case FireMode.Single:
                    ShootSingle(inputOrigin);
                    break;
                case FireMode.Burst:
                    StartCoroutine(ShootBurst(inputOrigin));
                    break;
                case FireMode.Auto:
                    StartCoroutine(ShootAuto(inputOrigin));
                    break;
                default: break;
            }
        }

        private void OnFireStop(FireInputOrigin inputOrigin)
        {
            if (!Master.IsShooting || Master.IsShootingBurst)
                return;

            Master.IsShooting = false;
        }

        private void OnInputInterrupted()
        {
            OnFireStop(new FireInputOrigin());
            Master.IsShootingBurst = false;
        }

        private void Start()
        {
            waitForNextAutoShoot = new WaitForSeconds(60.0f / Master.WeaponSettings.GunSettings.FireRate);
            waitForNextBurstShoot = new WaitForSeconds(60.0f / Master.WeaponSettings.GunSettings.BurstFireRate);
        }
    }
}
