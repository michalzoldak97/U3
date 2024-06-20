using UnityEngine;

namespace U3.Weapon.Effect
{
    [System.Serializable]
    public struct HitEffectSetting
    {
        public string HitEffectCode;
        public LayerMask[] HitLayers;
        public string[] EffectCodes;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HitEffectSettings", order = 7)]
    public class HitEffectSettings : ScriptableObject
    {
        public HitEffectSetting[] HitEffectConfig;
    }
}
