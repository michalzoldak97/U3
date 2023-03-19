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

        public delegate void ItemEventCallbackHandler();
        public event ItemEventCallbackHandler EventItemAdded;
        public event ItemEventCallbackHandler EventItemRemoved;

        public delegate void ItemSelectionEventHandler(Transform item);
        public event ItemSelectionEventHandler EventSelectItem;
        public event ItemSelectionEventHandler EventDeselectItem;
        public event ItemSelectionEventHandler EventItemSelected;
        public event ItemSelectionEventHandler EventItemDeselected;

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

        public void CallEventSelectItem(Transform item)
        {
            EventSelectItem?.Invoke(item);
        }

        public void CallEventDeselectItem(Transform item)
        {
            EventDeselectItem?.Invoke(item);
        }

        public void CallEventItemSelected(Transform item)
        {
            EventItemSelected?.Invoke(item);
        }

        public void CallEventItemDeselected(Transform item)
        {
            EventItemDeselected?.Invoke(item);
        }
    }
}
