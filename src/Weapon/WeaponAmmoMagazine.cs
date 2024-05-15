using U3.Core;
using U3.Item;

namespace U3.Weapon
{
    public class WeaponAmmoMagazine : Vassal<WeaponMaster>
    {
        private int ammoConsumption;

        private void OnEnable()
        {
            if (Master != null)
                SetWeaponLoaded();
        }

        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);
            Master.EventWeaponFired += SubstractAmmoOnShoot;
            Master.EventReloadFinnished += SetWeaponLoaded;
        }

        private void OnDisable()
        {
            Master.EventWeaponFired -= SubstractAmmoOnShoot;
            Master.EventReloadFinnished -= SetWeaponLoaded;
        }

        private void SubstractAmmoOnShoot(FireInputOrigin _)
        {
            Master.AmmoInMag -= ammoConsumption;

            if (Master.AmmoInMag < ammoConsumption)
                Master.IsLoaded = false;
        }

        private void SetWeaponLoaded()
        {
            if (Master.AmmoInMag >= ammoConsumption)
                Master.IsLoaded = true;
            else
                Master.IsLoaded = false;
        }

        private void Start()
        {
            Master.AmmoInMag = Master.WeaponSettings.AmmoSettings.InitialAmmo <= Master.WeaponSettings.AmmoSettings.MagazineSize ? Master.WeaponSettings.AmmoSettings.InitialAmmo : Master.WeaponSettings.AmmoSettings.MagazineSize;
            ammoConsumption = Master.WeaponSettings.AmmoSettings.ShootAmmoConsumption > 1 ? Master.WeaponSettings.AmmoSettings.ShootAmmoConsumption : 1;

            SetWeaponLoaded();
        }
    }
}
