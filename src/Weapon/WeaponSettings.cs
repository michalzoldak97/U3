using UnityEngine;

namespace U3.Weapon
{
    [System.Serializable]
    public struct AmmoSettings
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
    public struct GunSettings
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
        public float ReloadDurationSeconds = 3.0f;
        public float ShootStartOffset;
        public AmmoSettings AmmoSettings;
        public Vector3 WeaponAimPosition;
        public GunSettings GunSettings;
        public HitEffectSetting[] HitEffectSettings;
    }
}
