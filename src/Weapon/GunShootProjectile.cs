using U3.Destructible;
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
        private Vector3 startPos;
        private Transform m_Transform;
        protected virtual PooledObject GetProjectile()
        {
            return poolsManager.GetObject(Master.AmmoCode);
        }



        protected virtual void ShootProjectile(FireInputOrigin inputOrigin)
        {
            PooledObject projectile = GetProjectile();
            ObjectDamageManager.UpdateDamageInflictorOrigin(projectile.ObjInstanceID, inputOrigin.ID, inputOrigin.TeamID);

            // projectile.ObjTransform.SetPositionAndRotation(m_Transform.position + (m_Transform.forward * startPos.z), m_Transform.rotation);
            projectile.ObjTransform.SetPositionAndRotation(m_Transform.position + startPos, m_Transform.rotation);
            projectile.Obj.SetActive(true);
            projectile.ObjRigidbody.angularVelocity = Vector3.zero;
            projectile.ObjRigidbody.velocity = Vector3.zero;
            projectile.ObjRigidbody.AddForce(
                m_Transform.TransformDirection(
                    Random.Range(-0.01f, 0.01f),
                    Random.Range(-0.01f, 0.01f),
                    startPos.z) * shootForce, shootForceMode);
        }

        protected override void ShootAction(FireInputOrigin inputOrigin)
        {
            ShootProjectile(inputOrigin);
        }

        protected virtual void Start()
        {
            shootForceMode = Master.WeaponSettings.AmmoSettings.ShootForceMode;
            shootForce = Master.WeaponSettings.AmmoSettings.ShootForce;
            startPos = Master.WeaponSettings.ShootStartPosition;
            m_Transform = transform;
            poolsManager = ObjectPoolsManager.Instance; // TODO: reload in case of scene switch
        }
    }
}
