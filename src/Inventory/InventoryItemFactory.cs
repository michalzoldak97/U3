using U3.Item;
using U3.Log;
using UnityEngine;

namespace U3.Inventory
{
    public static class InventoryItemFactory
    {
        public static InventoryItem CreateInventoryItem(GameObject itemPrefab, IInventoryMaster inventoryMaster)
        {
            GameObject itemObject = Object.Instantiate(itemPrefab);
            if (itemObject.TryGetComponent(out ItemMaster _))
            {
                inventoryMaster.CallEventAddItem(itemObject.transform);
                return inventoryMaster.Items.GetItem(itemObject.transform);
            }
            else
            {
                GameLogger.Log(new GameLog(
                Log.LogType.Error,
                    $"Attempted to instantiate from prefab {itemPrefab.name} that is not an item"));
                return null;
            }
        }
    }
}
