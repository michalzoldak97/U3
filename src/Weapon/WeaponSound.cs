using U3.Core;
using U3.Item;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponSound : Vassal<WeaponMaster>
    {
        private bool hasAudioSource;
        private AudioClip shootSound;
        private AudioSource audioSource;

        public override void OnMasterEnabled(WeaponMaster master)
        {
            base.OnMasterEnabled(master);

            if (audioSource == null && TryGetComponent(out AudioSource audioS))
            {
                audioSource = audioS;
                hasAudioSource = true;
            }
            Master.EventWeaponFired += PlayShootSound;
            Master.EventReloadStarted += PlayReloadSound;
        }

        private void OnDisable()
        {
            Master.EventWeaponFired -= PlayShootSound;
            Master.EventReloadStarted -= PlayReloadSound;
        }

        private void PlayShootSound(FireInputOrigin _)
        {
            if (hasAudioSource)
                audioSource.PlayOneShot(shootSound);
            else
                AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }

        private void PlayReloadSound()
        {
            audioSource.PlayOneShot(Master.WeaponSettings.ReloadSound);
        }

        private void Start()
        {
            shootSound = Master.WeaponSettings.ShootSound;
        }
    }
}
