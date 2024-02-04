using System.Collections.Generic;
using System.Linq;
using U3.Global.Helper;
using U3.Inventory;
using U3.Item;
using U3.Log;
using UnityEngine;

namespace U3.Player.Inventory
{
    internal static class InventorySlotsInitializer
    {
        private static ItemSlotParent[] slotParents;

        private static PlayerInventoryMaster inventoryMaster;

        private static bool IsItemTypeAccepted(ItemType[] acceptableTypes, ItemType itemType)
        {
            if (acceptableTypes.Contains(itemType))
                return true;
            else
            {
                GameLogger.Log(new GameLog(Log.LogType.Error,
                    $"Unacceptable type {itemType} of the item assigned to the slot"));
                return false;
            }
        }

        private static void AssignItemToSlot(Transform item, IItemSlot slot)
        {
            InventoryItem itemToAssign = inventoryMaster.Items.GetItem(item);

            if (!IsItemTypeAccepted(slot.AcceptableItemTypes, itemToAssign.ItemMaster.ItemSettings.ItemType))
                return;

            ItemButtonFactory.AddItemButton(itemToAssign, inventoryMaster, slot);
            slot.AssignItem(itemToAssign);
        }

        private static bool SlotParentCodeExists(Dictionary<string, Transform> slotParents, string code)
        {
            if (!slotParents.Keys.Contains(code))
            {
                GameLogger.Log(new GameLog(Log.LogType.Error,
                    $"Inventory item slot with slot parent code {code} does not exists"));
                return false;
            }
            return true;
        }

        private static Dictionary<string, Transform> BuildSlotParentsSet()
        {
            Dictionary<string, Transform> slotParentsDict = new();
            foreach (ItemSlotParent sParent in slotParents)
            {
                slotParentsDict.Add(sParent.SlotCode, sParent.SlotParent);
            }
            return slotParentsDict;
        }

        private static void SetUpInventorySlots(InventorySlotSetting[] slotSettings)
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

        private static bool AreSlotCodesUnique()
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

        public static void Initialize(PlayerInventoryMaster inventoryMasterToSet, ItemSlotParent[] slotParentsToSet)
        {
            inventoryMaster = inventoryMasterToSet;
            slotParents = slotParentsToSet;

            if (!AreSlotCodesUnique())
                return;

            SetUpInventorySlots(inventoryMaster.PlayerMaster.PlayerSettings.Inventory.InventorySlots);

            inventoryMaster.CallEventSelectSlot(1);
        }
    }
}
