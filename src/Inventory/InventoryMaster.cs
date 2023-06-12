using System.Collections.Generic;
using UnityEngine;
using U3.Log;

namespace U3.Inventory
{
    public class InventoryMaster : MonoBehaviour, IInventoryMaster
    {
        public InventoryStore Items { get; } = new();

        public delegate void InventoryItemEventHandler(Transform item);
        public event InventoryItemEventHandler EventAddItem;
        public event InventoryItemEventHandler EventItemAdded;
        public event InventoryItemEventHandler EventRemoveItem;
        public event InventoryItemEventHandler EventItemRemoved;

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
