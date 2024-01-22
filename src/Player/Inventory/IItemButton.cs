using U3.Inventory;

namespace U3.Player.Inventory
{
    public interface IItemButton
    {
        public InventoryItem InventoryItem { get; set; }

        public IInventoryUIEventsMaster UIEventsMaster { get;  set; }

        public IInventoryDropArea ParentArea { get; }

        public void ChangeInventoryArea(IInventoryDropArea newArea);
    }
}
