using U3.Core;
using U3.Item;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponShoot : Vassal<WeaponMaster>
    {
        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            Master.EventFire += Shoot;
        }

        public override void OnMasterDisabled()
        {
            base.OnMasterDisabled();

            Master.EventFire -= Shoot;
        }

        private void OnDisable()
        {
            Master.EventFire -= Shoot;
        }

        private void Shoot(FireInputOrigin inputOrigin)
        {
            Debug.Log("PIF PAF");
            Master.CallEventWeaponFired(inputOrigin);
        }
    }
}
