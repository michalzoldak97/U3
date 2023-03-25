using UnityEngine;

namespace U3.Player.Inventory.UI
{
    public class Container : MonoBehaviour
    {
        public int[] ContainerIDX { get; set; } = new int[2];
        public PlayerInventoryMaster InventoryMaster { get; set; }

        public void ItemAdded(Transform item)
        {
            InventoryMaster.CallEventAddItemToContainer(ContainerIDX, item);
        }

        public void ItemRemoved(Transform item)
        {
            InventoryMaster.CallEventRemoveItemFromContainer(ContainerIDX, item);
        }
    }
}
