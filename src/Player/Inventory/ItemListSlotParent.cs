using UnityEngine;

namespace U3.Player.Inventory
{
    [System.Serializable]
    public class ItemListSlotParent
    {
        [SerializeField] private string slotCode;
        public string SlotCode => slotCode;
        public Transform SlotParent;
    }
}
