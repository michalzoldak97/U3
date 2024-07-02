using UnityEngine;

namespace U3.Damageable
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DamageTextSettings", order = 8)]
    public class DamageTextSettings : ScriptableObject
    {
        public int TextAnimationIterations;
        public float PosToGoUp;
        public float PosToGoRnd;
        public float LoopDelayDurationSec;
        public float OutlineWidth;
        public Color32 StartColor;
        public Color32 OutlineStartColor;
    }
}