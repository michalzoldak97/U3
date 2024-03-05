using System;
using System.Collections.Generic;
using System.Linq;
using U3.Inventory;
using U3.Item;
using UnityEngine;
using UnityEngine.InputSystem;

namespace U3.Player.Inventory
{
    public class InventorySlotsManager : MonoBehaviour
    {
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

            inventoryMaster.EventSelectSlot += OnSlotSelected;

            inventoryMaster.EventItemAdded += AssignItemToFreeSlot;

            inventoryMaster.EventAssignItemToFreeSlot += AssignItemToFreeSlot;
        }

        private void OnDisable()
        {
            selectSlot1.action.performed -= context => OnSlotSelected(1);
            selectSlot1.action.Disable();

            selectSlot2.action.performed -= context => OnSlotSelected(2);
            selectSlot2.action.Disable();

            selectSlot3.action.performed -= context => OnSlotSelected(3);
            selectSlot3.action.Disable();

            inventoryMaster.EventSelectSlot -= OnSlotSelected;

            inventoryMaster.EventItemAdded -= AssignItemToFreeSlot;

            inventoryMaster.EventItemAdded -= AssignItemToFreeSlot;

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

            IEnumerable<IItemSlot> freeSlots = inventoryMaster.ItemSlots.
                Where(slot => slot.AcceptableItemTypes.Contains(itemType) && slot.AssignedItem == null);

            if (freeSlots.Count() == 0)
                return (null, false);

            foreach (IItemSlot slot in freeSlots)
            {
                if (slot.IsSelected)
                    return (slot, true);
            }

            return (freeSlots.First(), true);
        }

        private bool AreSlotsSetUp()
        {
            return inventoryMaster.PlayerMaster.PlayerSettings.Inventory.InventorySlots.
                Where(slot => slot.IsSelectable).Count() == inventoryMaster.SelectableItemSlots.Count();
        }

        /// <summary>
        /// Assigns added item to slot if any available
        /// Notice it will be called on initialization too
        /// so 1st condition validates if it is initialization based on the AreSlotsSetUp()
        /// </summary>
        /// <param name="item"></param>
        private void AssignItemToFreeSlot(Transform item)
        {
            if (!AreSlotsSetUp())
                return;

            (IItemSlot slot, bool isAvailable) = GetAvailableSlot(item);
            if (!isAvailable)
                return;

            AssignItemToSlot(item, slot);
        }
    }
}
