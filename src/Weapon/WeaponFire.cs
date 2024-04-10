using U3.Global;
using U3.Item;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponFire : Vassal<WeaponMaster>
    {
        // on fire
        // if shooting do nothing
        // if reloading do nothing
        // if not loaded do click and wait
        // on disable reset states -> weapon mamager
        
        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            Master.EventFireDownCalled += OnFireStart;
            Master.EventFireUpCalled += OnFireStop;

            Master.EventInputInterrupted += () => OnFireStop(new FireInputOrigin());
        }

        public override void OnMasterDisabled()
        {
            base.OnMasterDisabled();

            Master.EventFireDownCalled -= OnFireStart;
            Master.EventFireUpCalled -= OnFireStop;

            Master.EventInputInterrupted -= () => OnFireStop(new FireInputOrigin());
        }

        public bool IsShootingBlocked()
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

        private void OnFireStart(FireInputOrigin inputOrigin)
        {
            Debug.Log($"Fire called by {inputOrigin.Name} with id {inputOrigin.ID} on weapon {gameObject.name} with id {transform.GetInstanceID()} is shooting {Master.IsShooting}");

            if (IsShootingBlocked())
                return;
        }

        private void OnFireStop(FireInputOrigin inputOrigin)
        {
            Debug.Log($"Fire STOP called by {inputOrigin.Name} with id {inputOrigin.ID} on weapon {gameObject.name} with id {transform.GetInstanceID()} is shooting {Master.IsShooting}");

            if (!Master.IsShooting)
                return;
        }
    }
}
