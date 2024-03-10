using U3.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace U3.Player.Inventory
{
    public class GunItemButton : ItemButton
    {
        [SerializeField] private Image itemIconImage;
        public override void SetUpButton(InventoryItem inventoryItem, IInventoryUIEventsMaster uIEventsMaster)
        {
            base.SetUpButton(inventoryItem, uIEventsMaster);

            itemIconImage.sprite = inventoryItem.ItemMaster.ItemSettings.ItemIcon;
        }
    }
}
