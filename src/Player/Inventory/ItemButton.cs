using U3.Inventory;
using UnityEngine;

namespace U3.Player.Inventory.Button
{
    public class ItemButton : MonoBehaviour, IItemButton
    {
        public InventoryItem InventoryItem { get; set; }

        public IInventoryUIEventsMaster UIEventsMaster { get; set; }

        public IInventoryDropArea ParentArea { get; private set; }

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