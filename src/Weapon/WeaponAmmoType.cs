using U3.Core;

namespace U3.Weapon
{
    public class WeaponAmmoType : Vassal<WeaponMaster>
    {
        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);
        }

        public override void OnMasterDisabled()
        {
            base.OnMasterDisabled();
        }

        private void Start()
        {
            Master.AmmoCode = Master.WeaponSettings.DefaultAmmoCode;
        }
    }
}
