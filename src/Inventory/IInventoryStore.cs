using UnityEngine;

namespace U3.Inventory
{
    public interface IInventoryStore
    {
        public bool AddItem(InventoryItem item);

        public int Count { get; } 

        public InventoryItem GetItem(Transform item);

        public InventoryItem[] GetAllItems();

        public void RemoveItem(Transform item);

        public void RemoveAllItems();
    }
}
