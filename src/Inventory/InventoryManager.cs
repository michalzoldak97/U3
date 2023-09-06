using U3.Item;
using UnityEngine;

namespace U3.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        protected InventoryMaster inventoryMaster;

        protected void SetInit()
        {
            inventoryMaster = GetComponent<InventoryMaster>();
        }

        protected void OnEnable()
        {
            SetInit();

            inventoryMaster.EventAddItem += AddItem;
            inventoryMaster.EventRemoveItem += RemoveItem;
            inventoryMaster.EventSelectItem += SelectItem;
            inventoryMaster.EventDeselectItem += DeselectItem;
            inventoryMaster.EventClearInventory += ClearInventory;
        }

        protected void OnDisable()
        {
            inventoryMaster.EventAddItem -= AddItem;
            inventoryMaster.EventRemoveItem -= RemoveItem;
            inventoryMaster.EventSelectItem -= SelectItem;
            inventoryMaster.EventDeselectItem -= DeselectItem;
            inventoryMaster.EventClearInventory -= ClearInventory;
        }

        private void AddItem(Transform item)
        {
            // check if item master exists
            if (item.TryGetComponent(out ItemMaster itemMaster))
            {
                // fetch and disable all rb and colliders
                // create item object and add to inventory, assign to parent transofrm
                // call EventAddedToInventory
                // evvaluate if should be selected
                // call event item has been added
            }
        }

        private void RemoveItem(Transform item)
        {
            // check if selected, if yes set selected to null or subsequent
            // enable rb and colliders
            // set parent to null
            // remove from inventory
            // call EventRemovedFromInventory
            // call event item has been removed 
            // call event item throw
        }

        private void SelectItem(Transform item)
        {
            // check if exists or already selected
            // deslect current selected
            // set active status and handle colliders and rb
            // call selected on item
            // call event selected
        }

        private void DeselectItem(Transform item)
        {
            // check if exists and selected
            // set active status and handle colliders and rb
            // call deselected on item
            // call deselected
        }

        private void ClearInventory()
        {
            // call remove on all items
        }
    }
}