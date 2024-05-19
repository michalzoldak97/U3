using UnityEngine;

namespace U3.Destructible
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DamageInflictorSettings", order = 6)]
    public class DamageInflictorSettings : ScriptableObject
    {
        public int RicochetLimit;
        public LayerMask LayersToDamage;
        public LayerMask LayersToHit;
        public DamageImpactType ImpactType;
        public DamageElementType ElementType;
        public float Damage;
        public float PenetrationCoeff;
        public float BaseVelocity;
        public Vector2 DamageEquation;
    }
}
