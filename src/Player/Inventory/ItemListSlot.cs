using U3.Inventory;
using U3.Item;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class ItemListSlot : MonoBehaviour, IInventoryDropArea, IInventoryItemSlot
    {
        public InventoryItem AssignedItem { get; set; }
        public ItemType[] AcceptableItemTypes { get; set; }

        public bool OnInventoryItemDrop(InventoryItem item)
        {
            // if item is of accepted type - swap current item with a new item
            // else
            return false;
        }
    }
}
