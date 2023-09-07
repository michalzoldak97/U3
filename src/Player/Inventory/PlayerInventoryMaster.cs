using UnityEngine;
using U3.Inventory;

namespace U3.Player.Inventory
{
    public class PlayerInventoryMaster : InventoryMaster
    {
        [SerializeField] private Transform itemContainer;

        // set active slot
        // add to slot (check if has been on another slot, if yes clear)
        public delegate void InventoryContainerEventHandler(Transform item, int[] idx);
        public event InventoryContainerEventHandler EventAssignItemToSlot;
        public event InventoryContainerEventHandler EventItemAssignedToSlot;

        public delegate void InventoryUIEventHandler();
        public event InventoryUIEventHandler EventSetActiveSlot; // if item on slot select it
        public event InventoryUIEventHandler EventInventoryScreenOpened;
        public event InventoryUIEventHandler EventInventoryScreenClosed;

        public void CallEventInventoryScreenOpened()
        {
            EventInventoryScreenOpened?.Invoke();
        }

        public void CallEventInventoryScreenClosed()
        {
            EventInventoryScreenClosed?.Invoke();
        }

        private void Awake()
        {
            ItemContainer = itemContainer;
        }
    }
}
