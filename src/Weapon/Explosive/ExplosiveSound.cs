using U3.Core;
using U3.Item;
using UnityEngine;

namespace U3.Weapon.Explosive
{
    public class ExplosiveSound : Vassal<ExplosiveMaster>
    {
        public override void OnMasterEnabled(ExplosiveMaster master)
        {
            base.OnMasterEnabled(master);

            Master.EventExploded += PlayExplosionSound;
        }

        private void OnDisable()
        {
            Master.EventExploded -= PlayExplosionSound;
        }

        private void PlayExplosionSound(FireInputOrigin _)
        {
            AudioSource.PlayClipAtPoint(Master.DmgSettings.ExplosionSound, transform.position);
        }
    }
}
