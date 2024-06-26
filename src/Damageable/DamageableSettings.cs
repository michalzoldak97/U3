using UnityEngine;

namespace U3.Damageable
{
    [System.Serializable]
    public struct HitBoxSetting
    {
        public float DamageMultiplayer;
        public float PenetrationReduction;
    }

    [System.Serializable]
    public struct HealthSetting
    {
        public float InitialHealth;
        public float Armor;
        public float ProjectileDamageMultiplayer;
        public float ExplosionDamageMultiplayer;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DamageableSettings", order = 5)]
    public class DamageableSettings : ScriptableObject
    {
        public HealthSetting HealthSetting;
        public HitBoxSetting HitBoxSetting;
    }
}
