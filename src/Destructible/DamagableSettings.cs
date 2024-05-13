using UnityEngine;

namespace U3.Destructible
{
    [System.Serializable]
    public struct HealthSetting
    {
        public float InitialHealth;
        public float Armor;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DamagableSettings", order = 5)]
    public class DamagableSettings : ScriptableObject
    {
        public HealthSetting HealthSetting;
    }
}
