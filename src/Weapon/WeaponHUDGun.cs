using System.Collections;
using TMPro;
using U3.Item;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponHUDGun : WeaponHUD
    {
        [SerializeField] private TMP_Text fireModeText;
        [SerializeField] private TMP_Text currentAmmoAmountText;
        [SerializeField] private TMP_Text currentAmmoNameText;

        private readonly WaitForEndOfFrame waitForEndOfFrame = new();

        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            Master.EventFireModeChanged += SetFireModeText;
            Master.EventWeaponFired += OnWeaponFireEvent;
            Master.EventReloadFinnished += TriggerChangeAmmoText;

            StartCoroutine(ChangeAmmoText());
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Master.EventFireModeChanged -= SetFireModeText;
            Master.EventWeaponFired -= OnWeaponFireEvent;
            Master.EventReloadFinnished -= TriggerChangeAmmoText;
        }

        private void SetFireModeText(FireMode fireMode)
        {
            fireModeText.text = fireMode.ToString();
        }

        private IEnumerator ChangeAmmoText()
        {
            yield return waitForEndOfFrame;
            if (Master.AmmoStore == null)
                yield break;

            WeaponAmmoData ammoData = Master.AmmoStore.GetAmmo(Master.AmmoCode);

            currentAmmoAmountText.text = $"{Master.AmmoInMag}/{ammoData.Amount}";
            currentAmmoNameText.text = $"{ammoData.Name}";
        }

        private void TriggerChangeAmmoText()
        {
            StartCoroutine(ChangeAmmoText());
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