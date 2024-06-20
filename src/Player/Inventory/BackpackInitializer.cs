using U3.Inventory;
using UnityEngine;

namespace U3.Player.Inventory
{
    internal static class BackpackInitializer
    {
        public static void Initialize(PlayerInventoryMaster inventoryMaster)
        {
            foreach (GameObject itemPrefab in inventoryMaster.PlayerMaster.PlayerSettings.Inventory.BackpackItems)
            {
                if (itemPrefab == null)
                    continue;

                InventoryItemFactory.CreateInventoryItem(itemPrefab, inventoryMaster);
            }

            inventoryMaster.IsBackpackInitialized = true;
        }
    }
}
