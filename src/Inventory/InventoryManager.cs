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

        private void DeselectItem(Transform itemTransform)
        {
            // check if exists and selected
            // set active status and handle colliders and rb
            // call deselected on item
            // call deselected
        }

        private void SelectItem(Transform itemTransform)
        {
            InventoryItem item = inventoryMaster.Items.GetItem(itemTransform);

            if (item == null ||
                item.Item == inventoryMaster.SelectedItem)
                return;

            if (inventoryMaster.SelectedItem != null)
                DeselectItem(inventoryMaster.SelectedItem);

            item.ItemObject.SetActive(true);
            inventoryMaster.SelectedItem = item.Item;

            item.ItemMaster.CallEventSelected();

            inventoryMaster.CallEventItemSelected(item.Item);
        }

        private void ToggleItemPhysics(InventoryItem item, bool toState)
        {
            foreach (Rigidbody rb in item.ItemRigidbodies)
            {
                rb.isKinematic = !toState;
                rb.useGravity = toState;
            }

            if (item.ItemMaster.ItemSettings.KeepColliderActive)
                return;

            foreach (Collider col in item.ItemColliders)
            {
                col.isTrigger = !toState;
            }
        }
    
        private void AddItem(Transform itemTransform)
        {
            if (itemTransform.TryGetComponent(out ItemMaster itemMaster))
            {
                InventoryItem newItem = new()
                {
                    Item = itemTransform,
                    ItemObject = itemTransform.gameObject,
                    ItemMaster = itemMaster
                };

                newItem.ItemRigidbodies = Engine.Engine.FetchAllComponents<Rigidbody>(newItem.ItemObject);
                newItem.ItemColliders = Engine.Engine.FetchAllComponents<Collider>(newItem.ItemObject);

                if (!inventoryMaster.Items.AddItem(newItem))
                    return;

                newItem.Item.SetParent(inventoryMaster.ItemContainer);

                ToggleItemPhysics(newItem, false);

                newItem.ItemMaster.CallEventAddedToInventory();

                inventoryMaster.CallEventItemAdded(newItem.Item);

                if (inventoryMaster.SelectedItem == null)
                    SelectItem(newItem.Item);
                else
                    DeselectItem(newItem.Item);
            }
        }

        private void RemoveItem(Transform itemTransform)
        {
            // check if selected, if yes set selected to null or subsequent
            // enable rb and colliders
            // set parent to null
            // remove from inventory
            // call EventRemovedFromInventory
            // call event item has been removed 
            // call event item throw
        }

        private void ClearInventory()
        {
            // call remove on all items
        }
    }
}