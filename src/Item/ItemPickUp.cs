using U3.Inventory;
using UnityEngine;

namespace U3.Item
{
    public class ItemPickUp : MonoBehaviour
    {
        private ItemMaster itemMaster;

        private void SetInit()
        {
            itemMaster = GetComponent<ItemMaster>();
        }

        private void OnEnable()
        {
            SetInit();

            itemMaster.EventInteractionCalled += PlaceOnInventory;
        }

        private void OnDisable()
        {
            itemMaster.EventInteractionCalled -= PlaceOnInventory;
        }

        private void PlaceOnInventory(Transform inventory)
        {
            if (inventory.TryGetComponent(out IInventoryMaster inventoryMaster))
                inventoryMaster.CallEventAddItem(transform);
        }
    }
}
