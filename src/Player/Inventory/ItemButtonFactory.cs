using U3.Inventory;
using U3.Log;
using UnityEngine;

namespace U3.Player.Inventory
{
    public static class ItemButtonFactory
    {
        public static void AddItemButton(InventoryItem inventoryItem, IInventoryUIEventsMaster uIEventsMaster, IInventoryDropArea dropArea)
        {
            GameObject uiPrefab = inventoryItem.ItemMaster.ItemSettings.InventoryUIPrefab;
            if (uiPrefab == null)
                GameLogger.Log(new GameLog(Log.LogType.Error,$"The {inventoryItem.Item.name} item is missing UI prefab"));

            GameObject itemObject = Object.Instantiate(uiPrefab, dropArea.ItemParentTransform);

            if (itemObject.TryGetComponent(out IItemButton itemButton))
            {
                itemButton.SetUpButton(inventoryItem, uIEventsMaster);
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