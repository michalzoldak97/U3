using System.Collections.Generic;
using System.Linq;
using U3.Global.Helper;
using U3.Inventory;
using U3.Log;
using UnityEngine;
using UnityEngine.InputSystem;

namespace U3.Player.Inventory
{
    public class InventorySlotsManager : MonoBehaviour
    {
        [SerializeField] private ItemSlotParent[] SlotParents;

        public InputActionReference selectSlot1;
        public InputActionReference selectSlot2;
        public InputActionReference selectSlot3;

        private PlayerInventoryMaster inventoryMaster;

        private void SetInit()
        {
            inventoryMaster = GetComponent<PlayerInventoryMaster>();
        }

        private void OnEnable()
        {
            SetInit();

            selectSlot1.action.Enable();
            selectSlot1.action.performed += context => OnSlotSelected(1);

            selectSlot2.action.Enable();
            selectSlot2.action.performed += context => OnSlotSelected(2);

            selectSlot3.action.Enable();
            selectSlot3.action.performed += context => OnSlotSelected(3);
        }

        private void OnDisable()
        {
            selectSlot1.action.performed -= context => OnSlotSelected(1);
            selectSlot1.action.Disable();

            selectSlot2.action.performed -= context => OnSlotSelected(2);
            selectSlot2.action.Disable();

            selectSlot3.action.performed -= context => OnSlotSelected(3);
            selectSlot3.action.Disable();

            inventoryMaster.PlayerMaster.UpdateInventorySettings();
        }

        private void OnSlotSelected(int slotIndex)
        {
            bool isSlotSelected = inventoryMaster.SelectableItemSlots[slotIndex].IsSelected;

            if (isSlotSelected)
                return;

            foreach (IItemSlot slot in inventoryMaster.SelectableItemSlots.Values)
            {
                slot.SetIsSelected(false);
            }

            inventoryMaster.SelectableItemSlots[slotIndex].SetIsSelected(true);

            // Call event slot selected
        }

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

        private void Start()
        {
            if (!SlotCodesAreUnique())
                return;

            SetUpInventorySlots(inventoryMaster.PlayerMaster.PlayerSettings.Inventory.InventorySlots);

            OnSlotSelected(1);
        }
    }
}
