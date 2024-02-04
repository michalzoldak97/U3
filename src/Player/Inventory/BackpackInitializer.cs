using U3.Inventory;
using UnityEngine;

namespace U3.Player.Inventory
{
    internal class BackpackInitializer
    {
        private readonly PlayerInventoryMaster inventoryMaster;

        public BackpackInitializer (PlayerInventoryMaster inventoryMaster)
        {
            this.inventoryMaster = inventoryMaster;
            Initialize();
        }

        private void Initialize()
        {
            foreach (GameObject itemPrefab in inventoryMaster.PlayerMaster.PlayerSettings.Inventory.BackpackItems)
            {
                if (itemPrefab == null)
                    continue;

                InventoryItemFactory.CreateInventoryItem(itemPrefab, inventoryMaster);
            }
        }
    }
}
