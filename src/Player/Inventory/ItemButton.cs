using TMPro;
using U3.Inventory;
using U3.Player.UI;
using UnityEngine;
using UnityEngine.UI;

namespace U3.Player.Inventory
{
    public class ItemButton : MonoBehaviour, IItemButton, IHoverable
    {
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private Image background;
        public InventoryItem InventoryItem { get; private set; }

        public IInventoryUIEventsMaster UIEventsMaster { get; private set; }

        public IInventoryDropArea ParentArea { get; private set; }

        private Color initialBackgroundColor = new();

        public void SetUpButton(InventoryItem inventoryItem, IInventoryUIEventsMaster uIEventsMaster)
        {
            InventoryItem = inventoryItem;
            UIEventsMaster = uIEventsMaster;

            itemName.text = InventoryItem.Item.name;
            initialBackgroundColor = background.color;
        }

        public void ChangeInventoryArea(IInventoryDropArea newArea)
        {
            if (ParentArea == null)
            {
                ParentArea = newArea;
                return;
            }

            if (!newArea.OnInventoryItemDrop(InventoryItem))
                return;

            transform.SetParent(newArea.AreaTransform);
            ParentArea.ItemRemovedFromArea(InventoryItem.Item);
            ParentArea = newArea;
        }

        public void OnPointerEnter()
        {
            UIEventsMaster.CallEventItemFocused(InventoryItem.Item);
            background.color = Color.cyan;
        }
        public void OnPointerExit()
        {
            UIEventsMaster.CallEventItemUnfocused(InventoryItem.Item);
            background.color = initialBackgroundColor;
        }

        private void OnDisable()
        {
            if (background != null)
                background.color = initialBackgroundColor;
        }
    }
}