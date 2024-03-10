namespace U3.Inventory
{
    public class InventoryStoreFactory
    {
        public IInventoryStore GetInventoryStore()
        {
            return new InventoryStore();
        }
    }
}
