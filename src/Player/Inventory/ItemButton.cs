using TMPro;
using U3.Inventory;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class ItemButton : MonoBehaviour, IItemButton
    {
        [SerializeField] private TMP_Text itemName;
        public InventoryItem InventoryItem { get; private set; }

        public IInventoryUIEventsMaster UIEventsMaster { get; private set; }

        public IInventoryDropArea ParentArea { get; private set; }

        public void SetUpButton(InventoryItem inventoryItem, IInventoryUIEventsMaster uIEventsMaster)
        {
            InventoryItem = inventoryItem;
            UIEventsMaster = uIEventsMaster;

            itemName.text = InventoryItem.Item.name;
        }

        public void ChangeInventoryArea(IInventoryDropArea newArea)
        {
            if (ParentArea == null)
            {
                ParentArea = newArea;
                return;
            }

            // inform parent area
        }

    }
}