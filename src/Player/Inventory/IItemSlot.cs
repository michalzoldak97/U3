using U3.Inventory;
using UnityEngine;

namespace U3.Player.Inventory
{
    public interface IItemSlot : IInventoryDropArea
    {
        public bool IsSelected { get; }
        public PlayerInventoryMaster InventoryMaster { get; set; }
        public InventoryItem AssignedItem { get; }
        public Item.ItemType[] AcceptableItemTypes { get; set; }

        public void SetIsSelected(bool isSelected);

        public void AssignItem(InventoryItem itemToAssign);

        public void UnassignItem(Transform itme);
    }
}
