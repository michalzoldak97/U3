using U3.Inventory;

namespace U3.Player.Inventory
{
    public interface IItemButton
    {
        public InventoryItem InventoryItem { get; }

        public IInventoryUIEventsMaster UIEventsMaster { get; }

        public IInventoryDropArea ParentArea { get; }

        public void SetUpButton(InventoryItem inventoryItem, IInventoryUIEventsMaster uIEventsMaster);

        public void ChangeInventoryArea(IInventoryDropArea newArea);
    }
}
