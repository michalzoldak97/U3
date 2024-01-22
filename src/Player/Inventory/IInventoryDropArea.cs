using U3.Inventory;
using UnityEngine;

namespace U3.Player.Inventory
{
    public interface IInventoryDropArea
    {
        public bool OnInventoryItemDrop(InventoryItem item);

        public Transform AreaTransform { get; }
    }
}
