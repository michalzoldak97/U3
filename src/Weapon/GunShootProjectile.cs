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

        protected virtual PooledObject GetProjectile()
        {
            return poolsManager.GetObject(Master.AmmoCode);
        }

        protected virtual void ShootProjectile(FireInputOrigin inputOrigin)
        {
            PooledObject projectile = GetProjectile();
            ObjectDamageManager.UpdateDamageInflictorOrigin(projectile.ObjInstanceID, inputOrigin.ID);
            projectile.ObjTransform.SetPositionAndRotation(m_Transform.TransformPoint(startPos), Quaternion.identity);
            projectile.Obj.SetActive(true);
            projectile.ObjRigidbody.angularVelocity = Vector3.zero;
            projectile.ObjRigidbody.velocity = Vector3.zero;

            Vector3 randVec = new Vector3(
                    Random.Range(-recoil.x, recoil.x),
                    Random.Range(-recoil.y, recoil.y),
                    0f);
            projectile.ObjRigidbody.AddForce(shootForce * m_Transform.forward + randVec, shootForceMode);
        }

        protected override void ShootAction(FireInputOrigin inputOrigin)
        {
            ShootProjectile(inputOrigin);
        }

        protected override void Start()
        {
            base.Start();
            shootForceMode = Master.WeaponSettings.AmmoSettings.ShootForceMode;
            shootForce = Master.WeaponSettings.AmmoSettings.ShootForce;
            poolsManager = ObjectPoolsManager.Instance; // TODO: reload in case of scene switch
        }
    }
}
