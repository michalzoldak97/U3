﻿using U3.Damageable;
using U3.ObjectPool;

namespace U3.Weapon
{
    public class GunShootProjectileAmmoBelt : GunShootProjectile
    {
        private int currentIndex;
        private string[] ammoSequence;

        protected override PooledObject<DamageInflictor> GetProjectile()
        {
            int idx = currentIndex;
            currentIndex = currentIndex + 1 >= ammoSequence.Length ? 0 : currentIndex + 1;

            return poolsManager.GetDamageInflictor(ammoSequence[idx]);
        }

        protected override void Start()
        {
            base.Start();

            ammoSequence = Master.WeaponSettings.AmmoSettings.AmmoBeltSequenceCodes;
        }
    }
}
