using System.Collections;
using TMPro;
using U3.Core;
using U3.Item;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponHUD : Vassal<WeaponMaster>
    {
        [SerializeField] private GameObject hudUI;

        [SerializeField] private TMP_Text fireModeText;
        [SerializeField] private TMP_Text currentAmmoAmountText;
        [SerializeField] private TMP_Text currentAmmoNameText;

        private readonly WaitForEndOfFrame waitForEndOfFrame = new();

        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            Master.ItemMaster.EventSelected += OnSelect;
            Master.ItemMaster.EventDeselected += ToggleOnDeselect;

            Master.EventFireModeChanged += SetFireModeText;
            Master.EventWeaponFired += OnWeaponFireEvent;
            Master.EventReloadFinnished += TriggerChangeAmmoText;
        }

        private void OnDisable()
        {
            Master.ItemMaster.EventSelected -= OnSelect;
            Master.ItemMaster.EventDeselected -= ToggleOnDeselect;

            Master.EventFireModeChanged -= SetFireModeText;
            Master.EventWeaponFired -= OnWeaponFireEvent;
            Master.EventReloadFinnished -= TriggerChangeAmmoText;
        }

        private void ToggleItemUI(bool toActive)
        {
            hudUI.SetActive(toActive);
        }

        private void SetFireModeText(FireMode fireMode)
        {
            fireModeText.text = fireMode.ToString();
        }

        private IEnumerator ChangeAmmoText()
        {
            yield return waitForEndOfFrame;

            WeaponAmmoData ammoData = Master.AmmoStore.GetAmmo(Master.AmmoCode);

            currentAmmoAmountText.text = $"{Master.AmmoInMag}/{ammoData.Amount}";
            currentAmmoNameText.text = $"{ammoData.Name}";
        }

        private void TriggerChangeAmmoText()
        {
            StartCoroutine(ChangeAmmoText());
        }

        private void OnSelect()
        {
            ToggleItemUI(true);
            TriggerChangeAmmoText();
        }

        private void ToggleOnDeselect()
        {
            ToggleItemUI(false);
        }

        private void OnWeaponFireEvent(FireInputOrigin _)
        {
            TriggerChangeAmmoText();
        }

        private void Start()
        {
            SetFireModeText(Master.WeaponSettings.GunSettings.DeafaultFireMode);
        }
    }
}
