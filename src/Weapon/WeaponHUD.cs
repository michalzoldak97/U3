using U3.Global;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponHUD : Vassal<WeaponMaster>
    {
        [SerializeField] private GameObject hudUI;

        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            Master.ItemMaster.EventSelected += () => ToggleItemUI(true);
            Master.ItemMaster.EventDeselected += () => ToggleItemUI(false);
        }

        public override void OnMasterDisabled()
        {
            base.OnMasterDisabled();

            Master.ItemMaster.EventSelected -= () => ToggleItemUI(true);
            Master.ItemMaster.EventDeselected -= () => ToggleItemUI(false);
        }

        private void ToggleItemUI(bool toActive)
        {
            hudUI.SetActive(toActive);
        }
    }
}
