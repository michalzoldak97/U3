using System;
using UnityEngine;

namespace U3.Weapon.Explosive
{
    public class ExplosiveImpact : MonoBehaviour
    {
        private Transform m_Transform;

        private float CalcDamage(float baseDmg, float minDmg, float radius, Vector3 hitPos)
        {
            float dist = Vector3.Distance(m_Transform.position, hitPos);
            float coeff = (minDmg - Mathf.Pow(radius, 3) - baseDmg) / radius;
            return radius * Mathf.Pow(dist, 2) + coeff * dist + baseDmg;
        }

        private void ApplyDamage()
        {
            // on event apply for each collider
        }

        private void Start()
        {
            m_Transform = transform;
        }
    }
}