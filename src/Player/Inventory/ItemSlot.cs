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
        public PlayerInventoryMaster InventoryMaster { get; set; }
        public InventoryItem AssignedItem { get; private set; }
        public ItemType[] AcceptableItemTypes { get; set; }

        public void SetIsSelected(bool isSelected)
        {
            IsSelected = isSelected;
            ChangeSlotSelection();
        }

        private bool IsItemAssigned(Transform item)
        {
            if (AssignedItem == null)
            {
                GameLogger.Log(new GameLog(Log.LogType.Warning, $"Attempt to unassign item {item.name} from an empty slot"));
                return false;
            }
            else if (AssignedItem.Item != item)
            {
                GameLogger.Log(new GameLog(Log.LogType.Warning, $"Item {item.name} is not assigned to the slot, assigned item is {AssignedItem.Item.name}"));
                return false;
            }

            return true;
        }

        public void UnassignItem(Transform item)
        {
            if (!IsItemAssigned(item))
                return;

            if (!AssignedItem.ItemObject.activeSelf)
                AssignedItem.ItemObject.SetActive(true);

            AssignedItem.ItemMaster.CallEventActionDeactivated();

            AssignedItem = null;

            if (InventoryMaster.Items.GetItem(item) != null)
                InventoryMaster.CallEventDeselectItem(item);

            InventoryMaster.CallEventReloadBackpack();
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

            if (AssignedItem == null)
                return;

            if (IsSelected)
                InventoryMaster.CallEventSelectItem(AssignedItem.Item);
            else
                InventoryMaster.CallEventDeselectItem(AssignedItem.Item);
        }
    }
}
