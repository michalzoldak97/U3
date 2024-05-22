using U3.Destructible;
using U3.ObjectPool;
using UnityEngine;

namespace U3.Weapon
{
    public class GunProjectile : DamageInflictor
    {
        private Rigidbody rb;
        private DamageInflictorReturner poolReturner;

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
            float pen = dmg * dmgSettings.PenetrationEquation.x;

            InflictDamage(col.transform, dmg, Random.Range(pen - dmgSettings.PenetrationEquation.y, pen + dmgSettings.PenetrationEquation.y));
        }

        private void OnCollisionEnter(Collision col)
        {
            int colLayer = col.gameObject.layer;

            if ((m_DmgData.LayersToDamage.value & (1 << colLayer)) != 0)
            {
                ApplyProjectileDamage(col);
                SpawnHitEffect(col);
            }
            else if ((m_DmgData.LayersToHit.value & (1 << colLayer)) != 0)
                SpawnHitEffect(col);

            poolReturner.ReturnToPool<DamageInflictor>();
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            poolReturner = GetComponent<DamageInflictorReturner>();
        }
    }
}
