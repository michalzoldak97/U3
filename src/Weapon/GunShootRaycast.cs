using U3.Destructible;
using U3.Item;
using UnityEngine;

namespace U3.Weapon
{
    public class GunShootRaycast : GunShoot
    {
        private DamageImpactType impactType;
        private DamageElementType elementType;
        private LayerMask layersToHit;
        private LayerMask layersToDamage;
        private float range;
        private Vector2 penEquation;
        private Vector2 dmgEquation;

        private float CalcPenetration(float dmg)
        {
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

            return dist * dmgEquation.x + dmgEquation.y;
        }

        private void ApplyDamage(FireInputOrigin inputOrigin, Transform hitTransform)
        {
            float dmg = CalcDamage(hitTransform);
            ObjectDamageManager.InflictDamage(hitTransform, new DamageData()
                {
                    InflictorID = inputOrigin.ID,
                    ImpactType = impactType,
                    ElementType = elementType,
                    RealDamage = dmg,
                    RealPenetration = CalcPenetration(dmg)
                });
        }

        protected void ShootRaycast(FireInputOrigin inputOrigin)
        {
            if (Physics.Raycast(
                m_Transform.TransformPoint(startPos),
                m_Transform.TransformDirection(
                    Random.Range(-recoil.x, recoil.x),
                    Random.Range(-recoil.y, recoil.y),
                    startPos.z
                ),
                out RaycastHit hit,
                range,
                layersToHit
            ))
            {
                int hitLayer = hit.transform.gameObject.layer;

                if ((layersToDamage & (1 << hitLayer)) != 0)
                {
                    ApplyDamage(inputOrigin, hit.transform);
                    // SpawnHitEffect(col);
                }
                else if ((layersToHit & (1 << hitLayer)) != 0)
                {
                    // SpawnHitEffect(col);
                }
            }
        }

        protected override void ShootAction(FireInputOrigin inputOrigin)
        {
            ShootRaycast(inputOrigin);
        }

        protected override void Start()
        {
            base.Start();
            layersToHit = Master.WeaponSettings.LayersToHit;
            layersToDamage = Master.WeaponSettings.LayersToDamage;
            range = Master.WeaponSettings.FireRange;
            penEquation = Master.WeaponSettings.RaycastDamageSettings.PenetrationEquation;
            dmgEquation = Master.WeaponSettings.RaycastDamageSettings.DamageEquation;
            impactType = Master.WeaponSettings.RaycastDamageSettings.ImpactType;
            elementType = Master.WeaponSettings.RaycastDamageSettings.ElementType;
        }
    }
}
