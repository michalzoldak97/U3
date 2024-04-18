using U3.Core;
using U3.Item;

namespace U3.Weapon
{
    public class WeaponAmmoMagazine : Vassal<WeaponMaster>
    {
        private int ammoConsumption;

        private void OnEnable()
        {
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
            Master.AmmoInMag = Master.WeaponSettings.InitialAmmo <= Master.WeaponSettings.MagazineSize ? Master.WeaponSettings.InitialAmmo : Master.WeaponSettings.MagazineSize;
            ammoConsumption = Master.WeaponSettings.ShootAmmoConsumption;

            SetWeaponLoaded();
        }
    }
}