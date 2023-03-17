using U3.Inventory;

namespace U3.Player.Inventory
{
    public class PlayerInventoryManager : InventoryManager
    {
        protected override void SetInit()
        {
            base.SetInit();
            itemContainer = GetComponent<PlayerMaster>().FPSCamera;
        }
    }
}
