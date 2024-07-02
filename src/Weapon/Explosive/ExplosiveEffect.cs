using U3.Core;
using U3.Weapon.Effect;
using U3.Item;

namespace U3.Weapon.Explosive
{
    public class ExplosiveEffect : Vassal<ExplosiveMaster>
    {
        public override void OnMasterEnabled(ExplosiveMaster master)
        {
            base.OnMasterEnabled(master);

            Master.EventExploded += PlayExplosionEffect;
        }

        private void OnDisable()
        {
            Master.EventExploded -= PlayExplosionEffect;
        }

        private void PlayExplosionEffect(FireInputOrigin _)
        {
            HitEffectManager.Instance.PlayEffect(transform.position, Master.DmgSettings.ExplosiveSetting.ExplosionEffectCode, Master.DmgSettings.EffectScale);
        }
    }
}