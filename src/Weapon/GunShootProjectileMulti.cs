using U3.Item;

namespace U3.Weapon
{
    public class GunShootProjectileMulti : GunShootProjectile
    {
        private int shootsCount;

        protected override void ShootAction(FireInputOrigin inputOrigin)
        {
            for (int i = 0; i < shootsCount; i++)
            {
                ShootProjectile(inputOrigin);
            }
        }

        protected override void Start()
        {
            base.Start();
            shootsCount = Master.WeaponSettings.ShootgunShootsCount;
        }
    }
}
