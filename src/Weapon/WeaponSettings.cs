using UnityEngine;

namespace U3.Weapon
{
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
        public GunShootType DefaultGunShootType;
        public GunShootType[] AvailableGunShootTypes;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponSettings", order = 3)]
    public class WeaponSettings : ScriptableObject
    {
        public int MagazineSize;
        public int InitialAmmo;
        public int ShootAmmoConsumption = 1;
        public float ReloadDurationSeconds = 3.0f;
        public string DefaultAmmoCode;
        public string[] AvailableAmmoCodes;
        public Vector3 WeaponAimPosition;
        public GunSettings GunSettings;
        public HitEffectSetting[] HitEffectSettings;
    }
}
