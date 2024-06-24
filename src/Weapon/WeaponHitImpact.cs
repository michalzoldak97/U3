using U3.Damageable;
using U3.Item;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponHitImpact : DamageInflictor
    {
        private Transform m_Transform;
        private WeaponMaster weaponMaster;

        private void OnEnable()
        {
            if (weaponMaster == null)
                weaponMaster = GetComponent<WeaponMaster>();

            weaponMaster.EventObjectHit += ApplyHitImpact;
        }

        private void OnDisable()
        {
            weaponMaster.EventObjectHit -= ApplyHitImpact;
        }

        private float CalcPenetration(float dmg)
        {
            Vector2 penEquation = dmgSettings.PenetrationEquation;
            if (penEquation.x == 0)
                return 1f;
            
            float pen = dmg * penEquation.x;
            return Random.Range(pen - penEquation.y, pen + penEquation.y);
        }

        private float CalcDamage(Transform hitTransform)
        {
            float dist = (m_Transform.position - hitTransform.position).magnitude;
            if (dist < 1)
                dist = 1;

            return dist * dmgSettings.DamageEquation.x + dmgSettings.DamageEquation.y;
        }

        private void ApplyHitImpact(FireInputOrigin inputOrigin, Transform hitTransform)
        {
            SetInflictorData(inputOrigin.ID, inputOrigin.LayersToHit, inputOrigin.LayersToDamage);

            float dmg = CalcDamage(hitTransform);
            InflictDamage(hitTransform, dmg, CalcPenetration(dmg));
        }

        private void Start()
        {
            m_Transform = transform;
        }
    }
}