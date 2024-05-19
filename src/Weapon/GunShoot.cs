using U3.Core;
using U3.Item;

namespace U3.Weapon
{
    public class GunShoot : Vassal<WeaponMaster>
    {
        protected Vector2 recoil;
        protected Vector3 startPos;
        protected Transform m_Transform;

        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            Master.EventFire += Shoot;
        }

        private void OnDisable()
        {
            Master.EventFire -= Shoot;
        }

        protected virtual void ShootAction(FireInputOrigin inputOrigin) { }

        private void Shoot(FireInputOrigin inputOrigin)
        {
            ShootAction(inputOrigin);
            Master.CallEventWeaponFired(inputOrigin);
        }

        protected virtual void Start()
        {
            recoil = Master.WeaponSettings.Recoil;
            startPos = Master.WeaponSettings.ShootStartPosition;
            m_Transform = transform;
        }
    }
}
