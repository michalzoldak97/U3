using System.Linq;
using U3.Inventory;
using U3.Item;
using UnityEngine;
using UnityEngine.UI;

namespace U3.Player.Inventory
{
    public class ItemSlot : MonoBehaviour, IInventoryDropArea, IItemSlot
    {
        public bool IsSelected { get; private set; }
        public RectTransform DropAreaTransform => slotRectTransform;
        public Transform ItemParentTransform => transform;
        public InventoryItem AssignedItem { get; private set; }
        public ItemType[] AcceptableItemTypes { get; set; }

        private RectTransform slotRectTransform;
        private PlayerInventoryMaster inventoryMaster;

        public void SetInventoryMaster(PlayerInventoryMaster inventoryMaster)
        {
            this.inventoryMaster = inventoryMaster;
            inventoryMaster.EventItemRemoved += UnassignItem;

            slotRectTransform = GetComponent<RectTransform>();
        }

        private void SetSlotColor()
        {
            if (IsSelected)
                GetComponent<Image>().color = inventoryMaster.PlayerMaster.PlayerSettings.Inventory.SlotSelectedColor;
            else
                GetComponent<Image>().color = inventoryMaster.PlayerMaster.PlayerSettings.Inventory.SlotDefaultColor;
        }

        private void ChangeSlotSelection()
        {
            SetSlotColor();

            if (AssignedItem == null)
                return;

            if (IsSelected)
                inventoryMaster.CallEventSelectItem(AssignedItem.Item);
            else
                inventoryMaster.CallEventDeselectItem(AssignedItem.Item);
        }

        public void SetIsSelected(bool isSelected)
        {
            IsSelected = isSelected;
            ChangeSlotSelection();
        }

        private void RemoveUIButton(Transform item)
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out IItemButton itemButton))
                {
                    if (itemButton.InventoryItem.Item == item)
                    {
                        Destroy(child.gameObject);
                        return;
                    }
                }
            }
        }

        private void UnassignItem(Transform item)
        {
            if (AssignedItem == null || AssignedItem.Item != item)
                return;

            RemoveUIButton(item);

            if (!AssignedItem.ItemObject.activeSelf)
                AssignedItem.ItemObject.SetActive(true);

            AssignedItem.ItemMaster.CallEventActionDeactivated();

            AssignedItem = null;

            if (inventoryMaster.Items.IsOnInventory(item))
                inventoryMaster.CallEventDeselectItem(item);
        }

        private void ChangeAssignedItem(InventoryItem toAssign)
        {
            if (toAssign == null)
                return;

            if (AssignedItem != null)
                UnassignItem(AssignedItem.Item);

            AssignedItem = toAssign;

            if (!AssignedItem.ItemObject.activeSelf)
                AssignedItem.ItemObject.SetActive(true);

            AssignedItem.ItemMaster.CallEventActionActivated();

            if (IsSelected)
                inventoryMaster.CallEventSelectItem(toAssign.Item);
            else
                inventoryMaster.CallEventDeselectItem(toAssign.Item);
        }

        public bool IsInventoryItemAccepted(InventoryItem item)
        {
            return AcceptableItemTypes.Contains(item.ItemMaster.ItemSettings.ItemType);
        }

        public void AssignInventoryItem(InventoryItem toAssign)
        {
            InventoryItem prevItem = AssignedItem;
            ChangeAssignedItem(toAssign);

            if (prevItem != null && inventoryMaster.Items.IsOnInventory(prevItem.Item))
                inventoryMaster.CallEventAssignItemToFreeSlot(prevItem.Item);
        }

        public void ItemRemovedFromArea(Transform item)
        {
            UnassignItem(item);
        }
    }
}
