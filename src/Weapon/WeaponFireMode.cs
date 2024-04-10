using U3.Global;

namespace U3.Weapon
{
    public class WeaponFireMode : Vassal<WeaponMaster>
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
            Master.FireMode = Master.WeaponSettings.GunSettings.DeafaultFireMode;
        }
    }
}
