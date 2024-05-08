using U3.ObjectPool;

namespace U3.Weapon
{
    public class GunShootProjectileAmmoBelt : GunShootProjectile
    {
        protected override PooledObject GetProjectile()
        {
            return base.GetProjectile();
        }
    }
}
