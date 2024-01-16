using UnityEngine;

namespace U3.Player.Inventory
{
    [System.Serializable]
    public class ItemSlotParent
    {
        [SerializeField] private string slotCode;
        public string SlotCode => slotCode;
        public Transform SlotParent;
    }
}
