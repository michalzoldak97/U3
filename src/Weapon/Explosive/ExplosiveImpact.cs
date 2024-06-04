using UnityEngine;
using U3.Core;

namespace U3.Weapon.Explosive
{
    public class ExplosiveImpact : Vassal<ExplosiveMaster>
    {
        private Transform m_Transform;

        private float GetDamage(float baseDmg, float minDmg, float radius, Vector3 hitPos)
        {
            float dist = Vector3.Distance(m_Transform.position, hitPos);
            float coeff = (minDmg - Mathf.Pow(radius, 3) - baseDmg) / radius;
            return radius * Mathf.Pow(dist, 2) + coeff * dist + baseDmg;
        }

        private void ApplyImpact()
        {
            for (int i = 0; i < Master.ExplosionTargets.Count; i++)
            {

            }
        }

        private void Start()
        {
            m_Transform = transform;
        }
    }
}