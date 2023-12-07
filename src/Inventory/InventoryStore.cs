using System.Collections.Generic;
using U3.Log;
using UnityEngine;

namespace U3.Inventory
{
    public class InventoryStore
    {
        private readonly Dictionary<Transform, InventoryItem> inventoryItems = new();

        public InventoryItem GetItem(Transform item)
        {
            if (!inventoryItems.ContainsKey(item))
            {
                GameLogger.Log(new GameLog(
                    Log.LogType.Warning, 
                    $"trying to access non-existing inventory item {item}"));
                return null;
            }

            return inventoryItems[item];
        }

        public InventoryItem[] GetAllItems()
        {
            InventoryItem[] allItems = new InventoryItem[inventoryItems.Count];

            int i = 0;
            foreach (InventoryItem item in inventoryItems.Values)
            {
                allItems[i] = item;
                i++;
            }

            return allItems;
        }

        public bool AddItem(InventoryItem item)
        {
            if (inventoryItems.ContainsKey(item.Item))
            {
                GameLogger.Log(new GameLog(
                    Log.LogType.Warning, 
                    $"trying to add duplicate inventory item {item.Item}"));
                return false;
            }

            inventoryItems.Add(item.Item, item);
            return true;
        }

        public void RemoveItem(Transform item)
        {
            if (!inventoryItems.ContainsKey(item))
            {
                GameLogger.Log(new GameLog(
                    Log.LogType.Warning, 
                    $"trying to remove non-existing inventory item {item}"));
                return;
            }

            inventoryItems.Remove(item);
        }

        public void RemoveAllItems()
        {
            inventoryItems.Clear();
        }
    }
}