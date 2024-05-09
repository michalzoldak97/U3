using U3.Item;
using U3.ObjectPool;
using UnityEngine;

namespace U3.Weapon
{
    public class GunShootProjectile : GunShoot
    {
        protected ObjectPoolsManager poolsManager;
        private ForceMode shootForceMode;
        private float shootForce;
        private float startOffset;
        private Transform m_Transform;
        protected virtual PooledObject GetProjectile()
        {
            return poolsManager.GetObject(Master.AmmoCode);
        }

        protected override void ShootAction(FireInputOrigin inputOrigin)
        {
            PooledObject projectile = GetProjectile();
            // set origin for damage inflictor

            projectile.ObjTransform.SetPositionAndRotation(m_Transform.position + (m_Transform.forward * startOffset), m_Transform.rotation);
            projectile.Obj.SetActive(true);
            projectile.ObjRigidbody.velocity = Vector3.zero;
            projectile.ObjRigidbody.angularVelocity = Vector3.zero;

            projectile.ObjRigidbody.AddForce(m_Transform.forward * shootForce, shootForceMode);
        }

        protected virtual void Start()
        {
            shootForceMode = Master.WeaponSettings.AmmoSettings.ShootForceMode;
            shootForce = Master.WeaponSettings.AmmoSettings.ShootForce;
            startOffset = Master.WeaponSettings.ShootStartOffset;
            m_Transform = transform;
            poolsManager = ObjectPoolsManager.Instance; // TODO: reload in case of scene switch
        }
    }
}
