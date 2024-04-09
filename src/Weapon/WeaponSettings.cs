using UnityEngine;

namespace U3.Weapon
{
    [System.Serializable]
    public struct GunSettings
    {
        public int FireRate;
        public GunFireMode FireMode;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponSettings", order = 3)]
    public class WeaponSettings : ScriptableObject
    {
        public GunSettings GunSettings;
    }
}
