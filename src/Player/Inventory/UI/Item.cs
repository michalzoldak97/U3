using TMPro;
using U3.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace U3.Player.Inventory.UI
{
    public class Item: MonoBehaviour
    {
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private Image itemIcon;

        public Transform InventoryItem { get; set; }

        private InventoryMaster inventoryMaster;

        public void SetItemName (string name)
        {
            itemName.text = name;
        }

        public void SetItemIcon(Sprite icon)
        {
            itemIcon.sprite = icon;
        }

        public void SetInventoryMaster(InventoryMaster inventoryMaster)
        {
            this.inventoryMaster = inventoryMaster;
        }

        public void SetUIParent(Transform toSet)
        {
            if (gameObject.TryGetComponent(out DraggableItemMaster iMaster))
            {
                iMaster.CallEventSlotOriginChanged(toSet);
            }
        }

        public void RemoveItemFromInventory()
        {
            if (InventoryItem == null)
                return;

            inventoryMaster.CallEventRemoveItem(InventoryItem);
        }
    }
}
