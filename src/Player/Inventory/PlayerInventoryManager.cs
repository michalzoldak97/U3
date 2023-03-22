using U3.Inventory;

namespace U3.Player.Inventory
{
    public class PlayerInventoryManager : InventoryManager
    {

        private PlayerInventoryMaster playerInventoryMaster;

        private void LoadInventorySlots(PlayerMaster playerMaster)
        {
            InventorySlotSetting[] slots = playerMaster.PlayerSettings.Inventory.InventorySlots;

            playerInventoryMaster.Slots = new Slot[slots.Length];

            for (int i = 0; i < slots.Length; i++)
            {
                playerInventoryMaster.Slots[i] = new Slot(slots[i].SlotName, slots[i].ContainerNum);
            }
        }
        protected override void SetInit()
        {
            inventoryMaster = GetComponent<PlayerInventoryMaster>();
            playerInventoryMaster = (PlayerInventoryMaster)inventoryMaster;

            PlayerMaster playerMaster = GetComponent<PlayerMaster>();
            itemContainer = playerMaster.FPSCamera;

            LoadInventorySlots(playerMaster);
        }
    }
}
