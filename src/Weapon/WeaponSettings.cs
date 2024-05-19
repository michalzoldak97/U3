﻿using UnityEngine;

namespace U3.Weapon
{
    [System.Serializable]
    public struct AmmoSetting
    {
        public int MagazineSize;
        public int InitialAmmo;
        public int ShootAmmoConsumption;
        public ForceMode ShootForceMode;
        public float ShootForce;
        public string DefaultAmmoCode;
        public string[] AmmoBeltSequenceCodes;
    }

    [System.Serializable]
    public class HitEffectSetting
    {
        public LayerMask HitEffectLayers;
        public string HitEffectCode;
    }

    [System.Serializable]
    public struct GunSetting
    {
        public int FireRate;
        public int BurstFireRate;
        public int ShootsInBurst;
        public FireMode DeafaultFireMode;
        public FireMode[] AvailableFireModes;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponSettings", order = 3)]
    public class WeaponSettings : ScriptableObject
    {
        public int ShootgunShootsCount;
        public LayerMask LayersToHit;
        public LayerMask LayersToDamage;
        public float ReloadDurationSeconds = 3.0f;
        public float FireRange;
        public AmmoSetting AmmoSettings;
        public Vector2 Recoil;
        public Vector3 WeaponAimPosition;
        public Vector3 ShootStartPosition;
        public GunSetting GunSettings;
        public DamageInflictorSettings RaycastDamageSettings;
        public HitEffectSetting[] HitEffectSettings;
    }
}
