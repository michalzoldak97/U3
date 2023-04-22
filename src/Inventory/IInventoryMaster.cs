using System.Collections.Generic;
using UnityEngine;

namespace U3.Inventory
{
    public interface IInventoryMaster
    {
        public Dictionary<Transform, InventoryItem> Items { get; set; }

        public GameObject ItemCamera {get; set;}

        public void CallEventAddItem(Transform item);
        public void CallEventRemoveItem(Transform item);

        public void CallEventItemAdded();
        public void CallEventItemRemoved();

        public void CallEventSelectItem(Transform item);
        public void CallEventDeselectItem(Transform item);
        public void CallEventItemSelected(Transform item);
        public void CallEventItemDeselected(Transform item);
    }
}
