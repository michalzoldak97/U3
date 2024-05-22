using UnityEngine;

namespace U3.Item
{
    public readonly struct FireInputOrigin
    {
        public int ID { get; }
        public LayerMask LayersToHit { get; }
        public LayerMask LayersToDamage { get; }

        public FireInputOrigin(int id, LayerMask toHitVal, LayerMask toDmgVal)
        {
            ID = id;
            LayersToHit = toHitVal;
            LayersToDamage = toDmgVal;
        }
    }
}
