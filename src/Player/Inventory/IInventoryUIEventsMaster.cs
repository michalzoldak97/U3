namespace U3.Player.Inventory
{
    interface IInventoryUIEventsMaster
    {
        public void CallEventOnItemButtonDrop(IItemButton itemButton, IInventoryDropArea dropArea);
    }
}
