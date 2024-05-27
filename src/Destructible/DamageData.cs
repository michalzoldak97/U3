using UnityEngine;

namespace U3.Destructible
{
    public struct DamageData
    {
        public int InflictorID;
        public LayerMask LayersToDamage;
        public LayerMask LayersToHit;
        public DamageImpactType ImpactType;
        public DamageElementType ElementType;
        public float RealDamage;
        public float RealPenetration;
        public float HitEffectScale;
        public string HitEffectSettingCode;
    }
}
