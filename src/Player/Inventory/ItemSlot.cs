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
        public Transform AreaTransform => transform;
        public PlayerInventoryMaster InventoryMaster { get; set; }
        public InventoryItem AssignedItem { get; private set; }
        public ItemType[] AcceptableItemTypes { get; set; }

        public void SetIsSelected(bool isSelected)
        {
            IsSelected = isSelected;
            ChangeSlotSelection();
        }

        public void AssignItem(InventoryItem toAssign)
        {
            // inform AssignedItem it is unassigned

            AssignedItem = toAssign;

            if (!AssignedItem.ItemObject.activeSelf)
                AssignedItem.ItemObject.SetActive(true);

            AssignedItem.ItemMaster.CallEventActionActivated();

            if (IsSelected)
                InventoryMaster.CallEventSelectItem(toAssign.Item);
            else
                InventoryMaster.CallEventDeselectItem(toAssign.Item);
        }

        public bool OnInventoryItemDrop(InventoryItem item)
        {
            if (!AcceptableItemTypes.Contains(item.ItemMaster.ItemSettings.ItemType))
                return false;

            AssignItem(item);
            return true;
        }

        private void ChangeSlotSelection()
        {
            //dev code to indicate selection
            if (IsSelected)
                GetComponent<Image>().color = new Color32(69, 128, 157, 178);
            else
                GetComponent<Image>().color = new Color32(0, 0, 0, 178);
        }
    }
}
