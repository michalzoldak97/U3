using U3.Inventory;
using U3.Log;
using UnityEngine;

namespace U3.Player.Inventory
{
    public static class ItemButtonFactory
    {
        public static void AddItemButton(InventoryItem inventoryItem, IInventoryUIEventsMaster uIEventsMaster, IInventoryDropArea dropArea)
        {
            GameObject itemObject = Object.Instantiate(inventoryItem.ItemMaster.ItemSettings.InventoryUIPrefab, dropArea.AreaTransform);

            if (itemObject.TryGetComponent(out IItemButton itemButton))
            {
                itemButton.InventoryItem = inventoryItem;
                itemButton.UIEventsMaster = uIEventsMaster;
                itemButton.ChangeInventoryArea(dropArea);
            }
            else
            {
                GameLogger.Log(new GameLog(
                Log.LogType.Error,
                    $"There is no mandatory IItemButton on the {itemObject.name} UI element"));
            }
        }
    }
}