using UnityEngine;

namespace U3.Inventory
{
    public class InventoryMaster : MonoBehaviour, IInventoryMaster
    {
        public Transform ItemContainer { get; protected set; }
        public Transform SelectedItem { get; set; }
        public InventoryStore Items { get; } = new();

        public delegate void InventoryItemEventHandler(Transform item);
        public event InventoryItemEventHandler EventAddItem;
        public event InventoryItemEventHandler EventItemAdded;
        public event InventoryItemEventHandler EventRemoveItem;
        public event InventoryItemEventHandler EventItemRemoved;

        public event InventoryItemEventHandler EventSelectItem;
        public event InventoryItemEventHandler EventItemSelected;
        public event InventoryItemEventHandler EventDeselectItem;
        public event InventoryItemEventHandler EventItemDeselected;

        public delegate void InventoryEventHandler();
        public event InventoryEventHandler EventClearInventory;
        public event InventoryEventHandler EventInventoryCleared;

        public void CallEventAddItem(Transform item)
        {
            EventAddItem?.Invoke(item);
        }
        public void CallEventItemAdded(Transform item)
        {
            EventItemAdded?.Invoke(item);
        }
        public void CallEventRemoveItem(Transform item)
        {
            EventRemoveItem?.Invoke(item);
        }
        public void CallEventItemRemoved(Transform item)
        {
            EventItemRemoved?.Invoke(item);
        }

        public void CallEventSelectItem(Transform item)
        {
            EventSelectItem?.Invoke(item);
        }
        public void CallEventItemSelected(Transform item)
        {
            EventItemSelected?.Invoke(item);
        }
        public void CallEventDeselectItem(Transform item)
        {
            EventDeselectItem?.Invoke(item);
        }
        public void CallEventItemDeselected(Transform item)
        {
            EventItemDeselected?.Invoke(item);
        }

        public void CallEventClearInventory()
        {
            EventClearInventory?.Invoke();
        }
        public void CallEventInventoryCleared()
        {
            EventInventoryCleared?.Invoke();
        }
    }
}
