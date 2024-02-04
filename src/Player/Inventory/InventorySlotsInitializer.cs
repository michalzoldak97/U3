using System.Collections.Generic;
using System.Linq;
using U3.Global.Helper;
using U3.Inventory;
using U3.Log;
using UnityEngine;

namespace U3.Player.Inventory
{
    internal class InventorySlotsInitializer
    {
        private readonly ItemSlotParent[] slotParents;

        private readonly PlayerInventoryMaster inventoryMaster;

        public InventorySlotsInitializer(PlayerInventoryMaster inventoryMaster, ItemSlotParent[] slotParents)
        {
            this.inventoryMaster = inventoryMaster;
            this.slotParents = slotParents;

            Initialize();
        }

        private void AssignItemToSlot(Transform item, IItemSlot slot)
        {
            InventoryItem itemToAssign = inventoryMaster.Items.GetItem(item);
            ItemButtonFactory.AddItemButton(itemToAssign, inventoryMaster, slot);
            slot.AssignItem(itemToAssign);
        }

        private bool SlotParentCodeExists(Dictionary<string, Transform> slotParents, string code)
        {
            if (!slotParents.Keys.Contains(code))
            {
                GameLogger.Log(new GameLog(
                Log.LogType.Error,
                    $"Inventory item slot with slot parent code {code} does not exists"));
                return false;
            }
            return true;
        }

        private Dictionary<string, Transform> BuildSlotParentsSet()
        {
            Dictionary<string, Transform> slotParents = new();
            foreach (ItemSlotParent sParent in this.slotParents)
            {
                slotParents.Add(sParent.SlotCode, sParent.SlotParent);
            }
            return slotParents;
        }

        private void SetUpInventorySlots(InventorySlotSetting[] slotSettings)
        {
            Dictionary<string, Transform> slotParents = BuildSlotParentsSet();

            foreach (InventorySlotSetting slotSetting in slotSettings)
            {
                if (!SlotParentCodeExists(slotParents, slotSetting.SlotUIParentCode))
                    continue;

                GameObject slot = Object.Instantiate(slotSetting.SlotUIPrefab, slotParents[slotSetting.SlotUIParentCode]);

                if (slot.TryGetComponent(out IItemSlot inventoryItemSlot))
                {
                    inventoryItemSlot.SetInventoryMaster(inventoryMaster);
                    inventoryItemSlot.AcceptableItemTypes = slotSetting.AcceptableItemTypes;

                    if (slotSetting.AssignedItem != null)
                        AssignItemToSlot(InventoryItemFactory.CreateInventoryItem(slotSetting.AssignedItem, inventoryMaster).Item, inventoryItemSlot);

                    inventoryMaster.ItemSlots.Add(inventoryItemSlot);

                    if (slotSetting.IsSelectable)
                        inventoryMaster.AddSelectableSlot(slotSetting.SlotIndex, inventoryItemSlot);
                }
                else
                {
                    GameLogger.Log(new GameLog(
                    Log.LogType.Error,
                        $"Inventory item slot list UI prefab {slot.name} does not implement mandatory interface IInventoryItemSlot"));
                }
            }
        }

        private bool AreSlotCodesUnique()
        {
            if (!Helper.IsPropertyUnique(slotParents, "SlotCode"))
            {
                GameLogger.Log(new GameLog(
                Log.LogType.Error,
                        $"Slot codes have to be unique"));
                return false;
            }
            return true;
        }

        private void Initialize()
        {
            if (!AreSlotCodesUnique())
                return;

            SetUpInventorySlots(inventoryMaster.PlayerMaster.PlayerSettings.Inventory.InventorySlots);

            inventoryMaster.CallEventSelectSlot(1);
        }
    }
}
