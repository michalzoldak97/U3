using U3.Damageable;
using U3.ObjectPool;
using UnityEngine;

namespace U3.Weapon
{
    public class GunProjectile : DamageInflictor
    {
        private Rigidbody rb;
        private DamageInflictorReturner poolReturner;

        private void CallHitEffect(int hitLayer, ContactPoint contact)
        {
            SpawnHitEffect(hitLayer, contact.point, contact.normal);
        }

        private float GetImpactVelocityMagnitude(Collision col)
        {
            Vector3 impulse = col.impulse;

            if (Vector3.Dot(col.GetContact(0).normal, impulse) < 0)
                impulse *= -1f;

            return (rb.linearVelocity - impulse / rb.mass).magnitude;
        }

        private void ApplyProjectileDamage(Collision col)
        {
            float dmg = (GetImpactVelocityMagnitude(col) / dmgSettings.ProjectileSetting.BaseVelocity) * dmgSettings.Damage;
            float pen = dmg * dmgSettings.PenetrationEquation.x;

            InflictDamage(col.transform, dmg, Random.Range(pen - pen * dmgSettings.PenetrationEquation.y, pen + pen * dmgSettings.PenetrationEquation.y));
        }

        private void OnCollisionEnter(Collision col)
        {
            int colLayer = col.gameObject.layer;

            if ((m_DmgData.LayersToDamage.value & (1 << colLayer)) != 0)
            {
                ApplyProjectileDamage(col);
                CallHitEffect(colLayer, col.GetContact(0));
            }
            else if ((m_DmgData.LayersToHit.value & (1 << colLayer)) != 0) 
            {
                CallHitEffect(colLayer, col.GetContact(0));
            }

            poolReturner.ReturnToPool<DamageInflictor>();
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            poolReturner = GetComponent<DamageInflictorReturner>();
        }
    }
}
