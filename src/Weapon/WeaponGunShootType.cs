using U3.Core;

namespace U3.Weapon
{
    public class WeaponGunShootType: Vassal<WeaponMaster>
    {
        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);
        }

        private void Start()
        {
            Master.GunShootType = Master.WeaponSettings.GunSettings.DefaultGunShootType;
        }
    }
}
