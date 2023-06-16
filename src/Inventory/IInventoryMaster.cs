using System.Collections.Generic;
using UnityEngine;

namespace U3.Inventory
{
    public interface IInventoryMaster
    {
        // props
        public Transform ItemContainer { get; } // parent transform for items
        public InventoryStore Items { get; } // protected dictionary of items

        // events
        public void CallEventAddItem(Transform item);
        public void CallEventItemAdded(Transform item);
        public void CallEventRemoveItem(Transform item);
        public void CallEventItemRemoved(Transform item);

        public void CallEventClearInventory();
        public void CallEventInventoryCleared();
    }
}
