using U3.Destructible;
using U3.Item;
using UnityEngine;

namespace U3.Weapon
{
    public class GunShootRaycast : GunShoot
    {
        private DamageImpactType impactType;
        private DamageElementType elementType;
        private float range;
        private float hitForce;
        private Vector2 penEquation;
        private Vector2 dmgEquation;
        private Vector3 effectScale;
        private string hitEffectSettingCode;

        private void ApplyHitForce(Rigidbody rb)
        {
            if (hitForce != 0 && rb != null)
                rb.AddForce(m_Transform.forward * hitForce, ForceMode.Impulse);
        }

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
                inputOrigin.LayersToHit
            ))
            {
                Transform hitTransform = hit.transform;
                int hitLayer = hitTransform.gameObject.layer;

                if ((inputOrigin.LayersToDamage.value & (1 << hitLayer)) != 0)
                {
                    ApplyDamage(inputOrigin, hitTransform);
                    ApplyHitForce(hit.rigidbody);
                    DestructibleEffectManager.Instance.FireHitEffect(hitLayer, hit.point, hit.normal, hitEffectSettingCode, effectScale);
                }
                else if ((inputOrigin.LayersToHit.value & (1 << hitLayer)) != 0)
                {
                    ApplyHitForce(hit.rigidbody);
                    DestructibleEffectManager.Instance.FireHitEffect(hitLayer, hit.point, hit.normal, hitEffectSettingCode, effectScale);
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
            range = Master.WeaponSettings.FireRange;
            hitForce = Master.WeaponSettings.HitDamageSettings.ImpactForce;
            penEquation = Master.WeaponSettings.HitDamageSettings.PenetrationEquation;
            dmgEquation = Master.WeaponSettings.HitDamageSettings.DamageEquation;
            impactType = Master.WeaponSettings.HitDamageSettings.ImpactType;
            elementType = Master.WeaponSettings.HitDamageSettings.ElementType;
            hitEffectSettingCode = Master.WeaponSettings.HitDamageSettings.ProjectileSetting.HitEffectSettingCode;
            effectScale = Master.WeaponSettings.HitDamageSettings.EffectScale;
        }
    }
}
