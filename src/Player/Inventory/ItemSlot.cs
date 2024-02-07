using System.Linq;
using U3.Inventory;
using U3.Item;
using U3.Log;
using UnityEngine;
using UnityEngine.UI;

namespace U3.Player.Inventory
{
    public class ItemSlot : MonoBehaviour, IInventoryDropArea, IItemSlot
    {
        public bool IsSelected { get; private set; }
        public Transform AreaTransform => transform;
        public InventoryItem AssignedItem { get; private set; }
        public ItemType[] AcceptableItemTypes { get; set; }

        private PlayerInventoryMaster inventoryMaster;

        public void SetInventoryMaster(PlayerInventoryMaster inventoryMaster)
        {
            this.inventoryMaster = inventoryMaster;
            inventoryMaster.EventItemRemoved += UnassignItem;
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

        private void RemoveUIButton()
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent<IItemButton>(out IItemButton _))
                    Destroy(child.gameObject);
            }
        }

        private void UnassignItem(Transform item)
        {
            if (AssignedItem == null || AssignedItem.Item != item)
                return;

            RemoveUIButton();

            if (!AssignedItem.ItemObject.activeSelf)
                AssignedItem.ItemObject.SetActive(true);

            AssignedItem.ItemMaster.CallEventActionDeactivated();

            AssignedItem = null;

            if (inventoryMaster.Items.GetItem(item) != null)
            {
                inventoryMaster.CallEventDeselectItem(item);
                inventoryMaster.CallEventItemAdded(item);
            }

            inventoryMaster.CallEventReloadBackpack();
        }

        public void AssignItem(InventoryItem toAssign)
        {
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

        public bool OnInventoryItemDrop(InventoryItem item)
        {
            if (!AcceptableItemTypes.Contains(item.ItemMaster.ItemSettings.ItemType))
                return false;

            AssignItem(item);
            return true;
        }

        public void ItemRemovedFromArea(Transform item)
        {
            UnassignItem(item);
        }
    }
}
