using U3.Core;
using U3.Item;

namespace U3.Weapon
{
    public class WeaponGunShoot : Vassal<WeaponMaster>
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

        private void GunShootRaycast(FireInputOrigin inputOrigin)
        {

        }

        private void GunShootProjectile(FireInputOrigin inputOrigin)
        {
            // set damage inflictor origin
        }

        private void Shoot(FireInputOrigin inputOrigin)
        {
            switch (Master.GunShootType)
            {
                case GunShootType.RaycastShoot:
                    GunShootRaycast(inputOrigin);
                    break;
                case GunShootType.ProjectileShoot:
                    GunShootProjectile(inputOrigin);
                    break;
                default:
                    break;
            }

            Master.CallEventWeaponFired(inputOrigin);
        }
    }
}
