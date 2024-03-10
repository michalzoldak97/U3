using U3.Inventory;
using UnityEngine;

namespace U3.Player.Inventory
{
    public interface IInventoryDropArea
    {
        public bool IsInventoryItemAccepted(InventoryItem item);
        public void AssignInventoryItem(InventoryItem item);

        public RectTransform DropAreaTransform { get; }
        public Transform ItemParentTransform { get; }

        public void ItemRemovedFromArea(Transform item);
    }
}
