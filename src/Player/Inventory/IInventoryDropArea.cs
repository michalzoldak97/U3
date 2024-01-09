using U3.Inventory;

namespace U3.Player.Inventory
{
    public interface IInventoryDropArea
    {
        public bool OnInventoryItemDrop(InventoryItem item);
    }
}
