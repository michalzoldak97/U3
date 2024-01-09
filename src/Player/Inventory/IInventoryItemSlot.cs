using U3.Inventory;

namespace U3.Player.Inventory
{
    public interface IInventoryItemSlot
    {
        public InventoryItem AssignedItem { get; set; }
        public Item.ItemType[] AcceptableItemTypes { get; set; }
    }
}
