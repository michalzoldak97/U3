﻿using UnityEngine;
using U3.AI.Team;

namespace U3.Damageable
{
    [System.Serializable]
    public struct MineSetting
    {
        public int FusePreassureKG;
        public TeamType InflictorTeam; // fetch correcponding team from static
        public LayerMask LayersToIgniteOn;
        public LayerMask LayersToDamage;
    }

    [System.Serializable]
    public struct HEATDamageSetting
    {
        public DamageImpactType ImpactType;
        public DamageElementType ElementType;
        public float FireRange;
        public Vector2 DamageEquation;
        public Vector2 PenetrationEquation;
    }

    [System.Serializable]
    public struct SplinterSetting
    {
        public int SplinterNum;
        public float SplinterRadius;
        public DamageImpactType SplinterImpactType;
        public DamageElementType SplinterElementType;
        public Vector2 SplinterDamageEquation;
        public Vector2 SplinterPenetrationEquation;
    }

    [System.Serializable]
    public struct ExplosiveInflictorSetting
    {
        public bool CheckPos;
        public bool CheckCorners;
        public int TargetCapacity;
        public float Radius;
        public float MinDamage;
        public float FuseDelaySeconds;
        public float FuseActivationDelaySeconds;
        public string ExplosionEffectCode;
        public Vector3 HEATDirection; // forward by default
        public SplinterSetting SplinterSetting;
        public HEATDamageSetting HEATDamageSetting;
    }

    [System.Serializable]
    public struct ProjectileInfilictorSetting
    {
        public int RicochetLimit;
        public float BaseVelocity;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DamageInflictorSettings", order = 6)]
    public class DamageInflictorSettings : ScriptableObject
    {
        public DamageImpactType ImpactType;
        public DamageElementType ElementType;
        public float Damage;
        public float ImpactForce;
        public float FireRange;
        public string HitEffectSettingCode;
        public Vector2 DamageEquation; // projectile: {coeff, inter}
        public Vector2 PenetrationEquation; // projectile: {%, var}, explosive: {base, var}
        public Vector3 EffectScale;
        public ProjectileInfilictorSetting ProjectileSetting;
        public ExplosiveInflictorSetting ExplosiveSetting;
        public MineSetting MineSetting;
        public AudioClip ExplosionSound;
    }
}
