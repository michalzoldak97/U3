using UnityEngine;

namespace U3.Destructible
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DamageInflictorSettings", order = 6)]
    public class DamageInflictorSettings : ScriptableObject
    {
        public int RicochetLimit;
        public DamageImpactType ImpactType;
        public DamageElementType ElementType;
        public float Damage;
        public float BaseVelocity;
        public Vector2 DamageEquation; // coeff, inter
        public Vector2 PenetrationEquation; // %, var
    }
}
