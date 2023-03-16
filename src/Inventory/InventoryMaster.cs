using System.Collections.Generic;
using UnityEngine;

namespace U3.Inventory
{
    public class InventoryMaster : MonoBehaviour, IInventoryMaster
    {
        public Dictionary<Transform, InventoryItem> Items { get; set; }

        public delegate void ItemRequestEventHandler(Transform item);
        public event ItemRequestEventHandler EventAddItem;
        public event ItemRequestEventHandler EventRemoveItem;

        public delegate void InventoryItemEventHandler();
        public event InventoryItemEventHandler EventItemAdded;
        public event InventoryItemEventHandler EventItemRemoved;
        public void CallEventAddItem(Transform item)
        {
            EventAddItem?.Invoke(item);
        }

        public void CallEventRemoveItem(Transform item)
        {
            EventRemoveItem?.Invoke(item);
        }
        public void CallEventItemAdded()
        {
            EventItemAdded?.Invoke();
        }

        public void CallEventItemRemoved()
        {
            EventItemRemoved?.Invoke();
        }
    }
}
