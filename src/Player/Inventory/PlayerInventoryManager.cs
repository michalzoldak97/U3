using U3.Inventory;
using U3.Item;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class PlayerInventoryManager : InventoryManager
    {

        private PlayerInventoryMaster playerInventoryMaster;
        // TODO: handle when picking up item when none is selected, it should be moved to 1st free slot or deactivated
        // hande adding and removing from slot
        private void LoadItems()
        {
            foreach (Transform iT in itemContainer)
            {
                if (iT.TryGetComponent<ItemMaster>(out _))
                {
                    inventoryMaster.CallEventAddItem(iT);
                }
            }
        }
        private void LoadInventorySlots()
        {
            InventorySlotSetting[] slots = GetComponent<PlayerMaster>().PlayerSettings.Inventory.InventorySlots;

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

            itemContainer = GetComponent<PlayerMaster>().FPSCamera;

            LoadInventorySlots();
            LoadItems();
        }
    }
}
