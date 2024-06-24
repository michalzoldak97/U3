using UnityEngine;

namespace U3.Damageable
{
    public struct DamageData
    {
        public int InflictorOriginID;
        public int InflictorInstanceID;
        public LayerMask LayersToDamage;
        public LayerMask LayersToHit;
        public DamageImpactType ImpactType;
        public DamageElementType ElementType;
        public float RealDamage;
        public float RealPenetration;
    }
}
