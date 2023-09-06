using UnityEngine;
using U3.Inventory;

namespace U3.Player.Inventory
{
    public class PlayerInventoryMaster : InventoryMaster
    {
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
    }
}
