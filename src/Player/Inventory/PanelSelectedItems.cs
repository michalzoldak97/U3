using U3.Inventory;
using U3.Log;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class PanelSelectedItems : MonoBehaviour
    {
        private PlayerInventoryMaster inventoryMaster;

        private InventoryItem CreateInventoryItem(GameObject itemPrefab)
        {
            // TODO: should use a factory
            return new InventoryItem();
        }

        private void SetUpInventorySlots(InventorySlotSetting[] slotSettings)
        {
            foreach (InventorySlotSetting slotSetting in slotSettings)
            {
                GameObject slot = Instantiate(slotSetting.SlotUIPrefab, slotSetting.SlotUIParent);

                if (slot.TryGetComponent(out IInventoryItemSlot inventoryItemSlot))
                {
                    if (slotSetting.AssignedItem != null)
                        inventoryItemSlot.AssignedItem = CreateInventoryItem(slotSetting.AssignedItem);

                    inventoryItemSlot.AcceptableItemTypes = slotSetting.AcceptableItemTypes;
                }
                else
                {
                    GameLogger.Log(new GameLog(
                    Log.LogType.Error,
                        $"Inventory item slot list UI prefab {slot.name} does not implement mandatory interface IInventoryItemSlot"));
                }
            }
        }
        private void SetInit()
        {
            if (inventoryMaster != null)
                return;

            if (transform.root.TryGetComponent(out PlayerInventoryMaster playerInventoryMaster))
            {
                inventoryMaster = playerInventoryMaster;
                SetUpInventorySlots(inventoryMaster.PlayerMaster.PlayerSettings.Inventory.InventorySlots);
            }
            else
            {
                GameLogger.Log(new GameLog(
                Log.LogType.Error,
                    $"There is no PlayerInventoryMaster on the PanelSelectedItems root"));
            }
        }
        private void OnEnable()
        {
            SetInit();
        }

        private void OnDisable()
        {
            // TODO: Update Slot Settings on the player
        }
    }
}
