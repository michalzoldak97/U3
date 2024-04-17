using UnityEngine;

namespace U3.Weapon
{
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
        public int MagazineSize;
        public int InitialAmmo;
        public int ShootAmmoConsumption = 1;
        public float ReloadDurationSeconds = 3.0f;
        public string DefaultAmmoCode;
        public string[] AvailableAmmoCodes;
        public GunSettings GunSettings;
    }
}
