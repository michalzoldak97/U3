using U3.Inventory;

namespace U3.Player.Inventory
{
    public interface IItemButton
    {
        public InventoryItem InventoryItem { get; set; }

        public IInventoryDropArea ParentArea { get; set; }

        public void ChangeInventoryArea(IInventoryDropArea newArea);
    }
}
