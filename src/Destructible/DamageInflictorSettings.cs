using UnityEngine;

namespace U3.Destructible
{
    [System.Serializable]
    public struct ExplosiveInflictorSetting
    {
        public bool CheckPos;
        public bool CheckCorners;
        public int TargetCapacity;
        public int SplinterNum;
        public float Radius;
        public float MinDamage;
        public Vector3 HEATDirection; // forward by default
        public string ExplosionEffectCode;
    }

    [System.Serializable]
    public struct ProjectileInfilictorSetting
    {
        public int RicochetLimit;
        public float BaseVelocity;
        public string HitEffectSettingCode;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DamageInflictorSettings", order = 6)]
    public class DamageInflictorSettings : ScriptableObject
    {
        public DamageImpactType ImpactType;
        public DamageElementType ElementType;
        public float Damage;
        public float ImpactForce;
        public Vector2 DamageEquation; // projectile: {coeff, inter}, HEAT/splinter: {val, var}
        public Vector2 PenetrationEquation; // projectile: {%, var}, explosive: {base, var} HEAT/splinter: {val, var}
        public Vector3 EffectScale;
        public ProjectileInfilictorSetting ProjectileSetting;
        public ExplosiveInflictorSetting ExplosiveSetting;
    }
}
