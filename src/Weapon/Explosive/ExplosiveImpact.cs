using UnityEngine;
using U3.Core;
using U3.Damageable;
using U3.Item;

namespace U3.Weapon.Explosive
{
    public class ExplosiveImpact : Vassal<ExplosiveMaster>
    {
        private float baseDmg;
        private float minDmg;
        private float radius;
        private Vector2 penEquation;
        private Transform m_Transform;

        public override void OnMasterEnabled(ExplosiveMaster master)
        {
            base.OnMasterEnabled(master);

            Master.EventIgniteExplosion += OnIgniteExplosion;
        }

        private void OnDisable()
        {
            Master.EventIgniteExplosion -= OnIgniteExplosion;
        }

        private float GetPenetration(float dmg)
        {
            float basePen = penEquation.x * (dmg / baseDmg);
            return Random.Range(basePen - basePen * penEquation.y, basePen + basePen * penEquation.y);
        }

        private float GetDamage(Vector3 hitPos)
        {
            float dist = Vector3.Distance(m_Transform.position, hitPos);
            float coeff = (minDmg - Mathf.Pow(radius, 3) - baseDmg) / radius;
            return radius * Mathf.Pow(dist, 2) + coeff * dist + baseDmg;
        }

        private void ApplyImpact((Collider targetCol, Vector3 hitPoint) target, int inflictorID, LayerMask layersToDamage)
        {
            if ((layersToDamage.value & (1 << target.targetCol.gameObject.layer)) != 0)
            {
                float dmg = GetDamage(target.hitPoint);
                DamageData dmgData = new()
                {
                    InflictorOriginID = inflictorID,
                    InflictorInstanceID = ObjectDamageManager.GetNextInflictorInstanceID(),
                    ImpactType = Master.DmgSettings.ImpactType,
                    ElementType = Master.DmgSettings.ElementType,
                    RealDamage = dmg,
                    RealPenetration = GetPenetration(dmg)
                };
                ObjectDamageManager.InflictDamage(target.targetCol.transform, dmgData);
            }

            if (target.targetCol.attachedRigidbody != null)
                target.targetCol.attachedRigidbody.AddExplosionForce(Master.DmgSettings.ImpactForce, m_Transform.position, radius, 0.0f, ForceMode.Impulse);
        }

        private void OnIgniteExplosion(FireInputOrigin origin)
        {
            for (int i = 0; i < Master.ExplosionTargets.Count; i++)
            {
                ApplyImpact(Master.ExplosionTargets[i], origin.ID, origin.LayersToDamage);
            }

            Master.CallEventExploded(origin);
        }

        private void Start()
        {
            baseDmg = Master.DmgSettings.Damage;
            minDmg = Master.DmgSettings.ExplosiveSetting.MinDamage;
            radius = Master.DmgSettings.ExplosiveSetting.Radius;
            penEquation = Master.DmgSettings.PenetrationEquation;
            m_Transform = transform;
        }
    }
}