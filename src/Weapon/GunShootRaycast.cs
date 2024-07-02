using U3.Item;
using U3.Weapon.Effect;
using UnityEngine;

namespace U3.Weapon
{
    public class GunShootRaycast : GunShoot
    {
        private float range;
        private float hitForce;

        private void ApplyHitForce(Rigidbody rb)
        {
            if (hitForce != 0 && rb != null)
                rb.AddForce(m_Transform.forward * hitForce, ForceMode.Impulse);
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
                    Master.CallEventObjectHit(inputOrigin, hitTransform);
                    ApplyHitForce(hit.rigidbody);
                    HitEffectManager.Instance.FireHitEffect(hitLayer, hit.point, hit.normal, Master.WeaponSettings.HitEffectSettingCode, Master.WeaponSettings.HitEffectScale);
                }
                else if ((inputOrigin.LayersToHit.value & (1 << hitLayer)) != 0)
                {
                    ApplyHitForce(hit.rigidbody);
                    HitEffectManager.Instance.FireHitEffect(hitLayer, hit.point, hit.normal, Master.WeaponSettings.HitEffectSettingCode, Master.WeaponSettings.HitEffectScale);
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
            hitForce = Master.WeaponSettings.ImpactForce;
        }
    }
}
