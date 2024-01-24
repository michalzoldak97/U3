using System.Collections.Generic;
using System.Linq;
using U3.Global.Helper;
using U3.Inventory;
using U3.Log;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class PanelSelectedItems : MonoBehaviour
    {
        [SerializeField] private ItemSlotParent[] SlotParents;

        private PlayerInventoryMaster inventoryMaster;

        private InventoryItem CreateInventoryItem(GameObject itemPrefab)
        {
            // TODO: should use a factory
            return new InventoryItem();
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
            foreach (ItemSlotParent sParent in SlotParents)
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

                GameObject slot = Instantiate(slotSetting.SlotUIPrefab, slotParents[slotSetting.SlotUIParentCode]);

                if (slot.TryGetComponent(out IItemSlot inventoryItemSlot))
                {
                    if (slotSetting.AssignedItem != null)
                        inventoryItemSlot.AssignedItem = CreateInventoryItem(slotSetting.AssignedItem);

                    inventoryItemSlot.InventoryMaster = inventoryMaster;
                    inventoryItemSlot.AcceptableItemTypes = slotSetting.AcceptableItemTypes;

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

        private bool SlotCodesAreUnique()
        {
            if (!Helper.IsPropertyUnique(SlotParents, "SlotCode"))
            {
                GameLogger.Log(new GameLog(
                Log.LogType.Error,
                        $"Slot codes on the {name} have to be unique"));
                return false;
            }
            return true;
        }

        private void SetInit()
        {
            if (inventoryMaster != null)
                return;

            if (!SlotCodesAreUnique())
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
                    $"There is no PlayerInventoryMaster on the {name} root"));
            }
        }
        private void OnEnable()
        {
            SetInit();
        }

        private void OnDisable()
        {
            inventoryMaster.PlayerMaster.UpdateInventorySettings();
        }
    }
}
