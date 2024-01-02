using UnityEngine;

namespace U3.Inventory
{
    public interface IInventoryStore
    {
        public InventoryItem GetItem(Transform item);

        public InventoryItem[] GetAllItems();

        public bool AddItem(InventoryItem item);

        public void RemoveItem(Transform item);

        public void RemoveAllItems();
    }
}
