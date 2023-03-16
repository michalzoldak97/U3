using System.Collections.Generic;
using UnityEngine;

namespace U3.Inventory
{
    public interface IInventoryMaster
    {
        public Dictionary<Transform, InventoryItem> Items { get; set; }

        public void CallEventAddItem(Transform item);
        public void CallEventRemoveItem(Transform item);

        public void CallEventItemAdded();
        public void CallEventItemRemoved();
    }
}
