using U3.Core;
using U3.Item;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponFire : Vassal<WeaponMaster>
    {
        // if not loaded do click and wait
        // on disable reset states -> weapon mamager
        
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
        }

        private bool IsShootingBlocked()
        {
            if (Master.IsShooting || Master.IsReloading)
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

        private void ShootBurst(FireInputOrigin inputOrigin)
        {

        }

        private void ShootAuto(FireInputOrigin inputOrigin)
        {

        }

        private void OnFireStart(FireInputOrigin inputOrigin)
        {
            Debug.Log($"Fire called by {inputOrigin.Name} with id {inputOrigin.ID} on weapon {gameObject.name} with id {transform.GetInstanceID()} is shooting {Master.IsShooting}");

            if (IsShootingBlocked())
                return;

            switch (Master.FireMode)
            {
                case FireMode.Single:
                    ShootSingle(inputOrigin);
                    break;
                case FireMode.Burst:
                    ShootBurst(inputOrigin);
                    break;
                case FireMode.Auto:
                    ShootAuto(inputOrigin);
                    break;
                default: break;
            }
        }

        private void OnFireStop(FireInputOrigin inputOrigin)
        {
            Debug.Log($"Fire STOP called by {inputOrigin.Name} with id {inputOrigin.ID} on weapon {gameObject.name} with id {transform.GetInstanceID()} is shooting {Master.IsShooting}");

            if (!Master.IsShooting)
                return;

            Master.IsShooting = false; // TODO: handle different fire modes
        }

        private void OnInputInterrupted()
        {
            OnFireStop(new FireInputOrigin());
        }
    }
}
