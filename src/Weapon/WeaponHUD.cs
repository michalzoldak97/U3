using TMPro;
using U3.Core;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponHUD : Vassal<WeaponMaster>
    {
        [SerializeField] private GameObject hudUI;

        [SerializeField] private TMP_Text fireModeText;

        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            Master.ItemMaster.EventSelected += () => ToggleItemUI(true);
            Master.ItemMaster.EventDeselected += () => ToggleItemUI(false);

            Master.EventFireModeChanged += SetFireModeText;
        }

        public override void OnMasterDisabled()
        {
            base.OnMasterDisabled();

            Master.ItemMaster.EventSelected -= () => ToggleItemUI(true);
            Master.ItemMaster.EventDeselected -= () => ToggleItemUI(false);

            Master.EventFireModeChanged -= SetFireModeText;
        }

        private void ToggleItemUI(bool toActive)
        {
            hudUI.SetActive(toActive);
        }

        private void SetFireModeText(FireMode fireMode)
        {
            fireModeText.text = fireMode.ToString();
        }

        private void Start()
        {
            SetFireModeText(Master.WeaponSettings.GunSettings.DeafaultFireMode);
        }
    }
}
