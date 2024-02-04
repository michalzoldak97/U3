using U3.Inventory;
using UnityEngine;

namespace U3.Player.Inventory
{
    public interface IItemSlot : IInventoryDropArea
    {
        public bool IsSelected { get; }
        public InventoryItem AssignedItem { get; }
        public Item.ItemType[] AcceptableItemTypes { get; set; }

        public void SetInventoryMaster(PlayerInventoryMaster inventoryMaster);

        public void SetIsSelected(bool isSelected);

        public void AssignItem(InventoryItem itemToAssign);
    }
}
