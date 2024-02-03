using System;
using System.Collections.Generic;
using System.Linq;
using U3.Global.Helper;
using U3.Inventory;
using U3.Item;
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

        private bool isInit;
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

            inventoryMaster.EventItemAdded += OnItemAdded;
        }

        private void OnDisable()
        {
            selectSlot1.action.performed -= context => OnSlotSelected(1);
            selectSlot1.action.Disable();

            selectSlot2.action.performed -= context => OnSlotSelected(2);
            selectSlot2.action.Disable();

            selectSlot3.action.performed -= context => OnSlotSelected(3);
            selectSlot3.action.Disable();

            inventoryMaster.EventItemAdded -= OnItemAdded;

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

            if (inventoryMaster.SelectableItemSlots[slotIndex].AssignedItem == null)
                return;

            inventoryMaster.CallEventSlotSelected(inventoryMaster.SelectableItemSlots[slotIndex].AssignedItem.Item);
        }

        private void AssignItemToSlot(Transform item, IItemSlot slot)
        {
            InventoryItem itemToAssign = inventoryMaster.Items.GetItem(item);
            ItemButtonFactory.AddItemButton(itemToAssign, inventoryMaster, slot);
            slot.AssignItem(itemToAssign);
        }

        private (IItemSlot slot, bool isAvailable) GetAvailableSlot(Transform item)
        {
            ItemType itemType = inventoryMaster.Items.GetItem(item).ItemMaster.ItemSettings.ItemType;

            IEnumerable<IItemSlot> freeSlots = inventoryMaster.ItemSlots.Where(slot => slot.AcceptableItemTypes.Contains(itemType) && slot.AssignedItem == null);
            if (freeSlots.Count() == 0)
                return (null, false);

            foreach (IItemSlot slot in freeSlots)
            {
                if (slot.IsSelected)
                    return (slot, true);
            }

            return (freeSlots.First(), true);
        }

        /// <summary>
        /// Assigns added item to slot if any available
        /// Notice it will be called on initialization too
        /// so 1st condition validates if it is initialization based on the isInit
        /// </summary>
        /// <param name="item"></param>
        private void OnItemAdded(Transform item)
        {
            if (isInit)
                return;

            (IItemSlot slot, bool isAvailable) = GetAvailableSlot(item);
            if (!isAvailable)
                return;

            AssignItemToSlot(item, slot);
        }

        private void UnassignItemFromSlot(Transform item, IItemSlot slot)
        {
            // inform slot
            // slot should
                // call deactivate
                // remove button
        }

        private void OnItemRemoved(Transform item)
        {
            // check if was assigned to slot
            // if assigned 
                // UnassignItemFromSlot
        }

        private InventoryItem CreateInventoryItem(GameObject itemPrefab)
        {
            // TODO: test it dummy
            GameObject itemObject = Instantiate(itemPrefab);
            if (itemObject.TryGetComponent(out ItemMaster _))
            {
                inventoryMaster.CallEventAddItem(itemObject.transform);
                return inventoryMaster.Items.GetItem(itemObject.transform);
            }
            else
            {
                GameLogger.Log(new GameLog(
                Log.LogType.Error,
                    $"{name} attempted to instantiate from prefab {itemPrefab.name} that is not an item"));
                return null;
            }
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
                    inventoryItemSlot.InventoryMaster = inventoryMaster;
                    inventoryItemSlot.AcceptableItemTypes = slotSetting.AcceptableItemTypes;

                    if (slotSetting.AssignedItem != null)
                        AssignItemToSlot(CreateInventoryItem(slotSetting.AssignedItem).Item, inventoryItemSlot);

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

            isInit = true;
            SetUpInventorySlots(inventoryMaster.PlayerMaster.PlayerSettings.Inventory.InventorySlots);
            isInit = false;

            OnSlotSelected(1);
        }
    }
}
