using U3.Inventory;
using U3.Log;
using UnityEngine;

namespace U3.Player.Inventory
{
    public static class ItemDetailsFactory
    {
        public static void AddItemDetails(InventoryItem inventoryItem, Transform detailsParent)
        {
            GameObject detailsPrefab = inventoryItem.ItemMaster.ItemSettings.InventoryUIDetailsPrefab;

            if (detailsPrefab == null || !detailsPrefab.TryGetComponent(out IItemDetails _))
            {
                GameLogger.Log(new GameLog(Log.LogType.Error, $"invalid UI prefab for the {inventoryItem.Item.name} item"));
                return;
            }

            GameObject itemDetails = Object.Instantiate(detailsPrefab, detailsParent);

            itemDetails.GetComponent<IItemDetails>().SetItemSettings(inventoryItem.ItemMaster.ItemSettings);
        }
    }
}
