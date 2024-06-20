using UnityEngine;

namespace U3.Item
{
    public interface IAimable
    {
        public float ItemParentAimFOV { get; }
        public Vector3 ItemParentAimPosition { get; }
    }
}
