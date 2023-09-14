using UnityEngine;
using U3.Inventory;

namespace U3.Player.Inventory
{
    public class PlayerInventoryMaster : InventoryMaster
    {
        [SerializeField] private Transform itemContainer;
        // slot can be selectable or not, can have different item types accepted
        // IInventorySlot (?) 
        // container should have label, should be selectable or not
        // set active slot
        // add to slot (check if has been on another slot, if yes clear)
        public delegate void InventoryContainerEventHandler(Transform item, int[] idx);
        public event InventoryContainerEventHandler EventSetActiveContainer; // unselect previous container and slot, select the 1st selectable slot with item attached 
        public event InventoryContainerEventHandler EventSetActiveSlot; // if item on slot select it
        public event InventoryContainerEventHandler EventAssignItemToSlot; // if item assigned to other remove it from other, if -1 -1 (its moved back) or inactive -> deselect it, if moved on active slot select it 
        public event InventoryContainerEventHandler EventItemAssignedToSlot;

        public delegate void InventoryUIEventHandler();
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
