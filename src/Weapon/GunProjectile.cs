using U3.Destructible;
using U3.ObjectPool;
using UnityEngine;

namespace U3.Weapon
{
    public class GunProjectile : DamageInflictor
    {
        private Rigidbody rb;
        private ReturnToObjectPool poolReturner;

        private float GetImpactVelocityMagnitude(Collision col)
        {
            Vector3 impulse = col.impulse;

            if (Vector3.Dot(col.GetContact(0).normal, impulse) < 0)
                impulse *= -1f;

            return (rb.velocity - impulse / rb.mass).magnitude;
        }

        private void ApplyProjectileDamage(Collision col)
        {
            float dmg = (GetImpactVelocityMagnitude(col) / dmgSettings.BaseVelocity) * dmgSettings.Damage;
            float pen = dmg * dmgSettings.PenetrationCoeff;

            InflictDamage(col.transform, dmg, pen);
        }

        private void OnCollisionEnter(Collision col)
        {
            ApplyProjectileDamage(col);
            poolReturner.ReturnToPool();
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            poolReturner = GetComponent<ReturnToObjectPool>();
        }
    }
}
