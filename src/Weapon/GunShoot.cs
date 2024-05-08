using U3.Core;
using U3.Item;

namespace U3.Weapon
{
    public class GunShoot : Vassal<WeaponMaster>
    {
        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            Master.EventFire += Shoot;
        }

        private void OnDisable()
        {
            Master.EventFire -= Shoot;
        }

        protected virtual void ShootAction(FireInputOrigin inputOrigin) { }

        private void Shoot(FireInputOrigin inputOrigin)
        {
            ShootAction(inputOrigin);
            Master.CallEventWeaponFired(inputOrigin);
        }
    }
}
