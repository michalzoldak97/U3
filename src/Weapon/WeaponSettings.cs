using UnityEngine;

namespace U3.Weapon
{
    [System.Serializable]
    public struct GunSettings
    {
        public uint FireRate;
        public FireMode DeafaultFireMode;
        public FireMode[] AvailableFireModes;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponSettings", order = 3)]
    public class WeaponSettings : ScriptableObject
    {
        public uint MagazineSize;
        public uint InitialAmmo;
        public uint ShootAmmoConsumption = 1;
        public string DefaultAmmoCode;
        public string[] AvailableAmmoCodes;
        public GunSettings GunSettings;
    }
}
