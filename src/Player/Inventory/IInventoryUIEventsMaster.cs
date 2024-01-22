namespace U3.Player.Inventory
{
    public interface IInventoryUIEventsMaster
    {
        public void CallEventOnItemButtonDrop(IItemButton itemButton, IInventoryDropArea dropArea);
    }
}
