using U3.Inventory;

namespace U3.Player.Inventory
{
    public interface IItemSlot
    {
        public bool IsSelected { get; set; }
        public PlayerInventoryMaster InventoryMaster { get; set; }
        public InventoryItem AssignedItem { get; set; }
        public Item.ItemType[] AcceptableItemTypes { get; set; }
    }
}
