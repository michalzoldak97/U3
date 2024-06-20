using U3.Core;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponHUD : Vassal<WeaponMaster>
    {
        [SerializeField] private GameObject hudUI;

        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            Master.ItemMaster.EventSelected += OnSelect;
            Master.ItemMaster.EventDeselected += ToggleOnDeselect;
        }

        protected virtual void OnDisable()
        {
            Master.ItemMaster.EventSelected -= OnSelect;
            Master.ItemMaster.EventDeselected -= ToggleOnDeselect;
        }

        private void ToggleItemUI(bool toActive)
        {
            hudUI.SetActive(toActive);
        }

        protected virtual void OnSelect()
        {
            ToggleItemUI(true);
        }

        private void ToggleOnDeselect()
        {
            ToggleItemUI(false);
        }
    }
}
