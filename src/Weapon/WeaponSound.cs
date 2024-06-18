using U3.Core;
using U3.Item;
using U3.Log;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponSound : Vassal<WeaponMaster>
    {
        private AudioClip shootSound;
        private AudioSource audioSource;

        public override void OnMasterEnabled(WeaponMaster master)
        {
            base.OnMasterEnabled(master);

            if (audioSource == null)
            {
                if (TryGetComponent(out AudioSource audioS))
                    audioSource = audioS;
                else
                    GameLogger.Log(new GameLog(Log.LogType.Error, $"Required AudioSource not found on object {gameObject.name}"));
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
            audioSource.PlayOneShot(shootSound);
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
