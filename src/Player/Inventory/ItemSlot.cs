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
        public InventoryItem AssignedItem { get; set; }
        public ItemType[] AcceptableItemTypes { get; set; }

        public void SetIsSelected(bool isSelected)
        {
            IsSelected = isSelected;
            ChangeSlotSelection();
        }

        public bool OnInventoryItemDrop(InventoryItem item)
        {
            // if item is of accepted type - swap current item with a new item
            // else
            return false;
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
