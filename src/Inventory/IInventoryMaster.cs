using UnityEngine;

namespace U3.Inventory
{
    public interface IInventoryMaster
    {
        // props
        public Transform ItemContainer { get; } // parent transform for items
        public Transform SelectedItem { get; set; } // item active on parent
        public InventoryStore Items { get; } // protected dictionary of items

        // events
        public void CallEventAddItem(Transform item);
        public void CallEventItemAdded(Transform item);
        public void CallEventRemoveItem(Transform item);
        public void CallEventItemRemoved(Transform item);

        public void CallEventSelectItem(Transform item);
        public void CallEventItemSelected(Transform item);
        public void CallEventDeselectItem(Transform item);
        public void CallEventItemDeselected(Transform item);

        public void CallEventClearInventory();
        public void CallEventInventoryCleared();
    }
}
