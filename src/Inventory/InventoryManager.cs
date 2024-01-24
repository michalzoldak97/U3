using U3.Item;
using U3.Global.Helper;
using UnityEngine;

namespace U3.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        protected InventoryMaster inventoryMaster;

        protected virtual void SetInit()
        {
            inventoryMaster = GetComponent<InventoryMaster>();
        }

        protected virtual void OnEnable()
        {
            SetInit();

            inventoryMaster.EventAddItem += AddItem;
            inventoryMaster.EventRemoveItem += RemoveItem;
            inventoryMaster.EventSelectItem += SelectItem;
            inventoryMaster.EventDeselectItem += DeselectItem;
            inventoryMaster.EventClearInventory += ClearInventory;
        }

        protected virtual void OnDisable()
        {
            inventoryMaster.EventAddItem -= AddItem;
            inventoryMaster.EventRemoveItem -= RemoveItem;
            inventoryMaster.EventSelectItem -= SelectItem;
            inventoryMaster.EventDeselectItem -= DeselectItem;
            inventoryMaster.EventClearInventory -= ClearInventory;
        }

        private void DeselectItem(Transform itemTransform)
        {
            InventoryItem item = inventoryMaster.Items.GetItem(itemTransform);
            if (item == null)
                return;

            item.ItemMaster.CallEventDeselected();
            item.ItemObject.SetActive(false);

            inventoryMaster.CallEventItemDeselected(item.Item);
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

                newItem.ItemRigidbodies = Helper.FetchAllComponents<Rigidbody>(newItem.ItemObject);
                newItem.ItemColliders = Helper.FetchAllComponents<Collider>(newItem.ItemObject);

                if (!inventoryMaster.Items.AddItem(newItem))
                    return;

                newItem.Item.SetParent(inventoryMaster.ItemContainer);
                newItem.ItemMaster.CallEventAddedToInventory();

                ToggleItemPhysics(newItem, false);
                DeselectItem(newItem.Item);

                inventoryMaster.CallEventItemAdded(newItem.Item);
            }
        }

        private void RemoveItem(Transform itemTransform)
        {
            if (itemTransform == inventoryMaster.SelectedItem)
            {
                DeselectItem(itemTransform);
                inventoryMaster.SelectedItem = null;
            }

            InventoryItem item = inventoryMaster.Items.GetItem(itemTransform);
            if (item == null)
                return;

            ToggleItemPhysics(item, true);

            item.Item.SetParent(null);
            item.ItemObject.SetActive(true);

            inventoryMaster.Items.RemoveItem(item.Item);

            item.ItemMaster.CallEventRemovedFromInventory();

            inventoryMaster.CallEventItemRemoved(item.Item);

            item.ItemMaster.CallEventThrow(inventoryMaster.ItemContainer);
        }

        private void ClearInventory()
        {
            InventoryItem[] allItems = inventoryMaster.Items.GetAllItems();

            foreach (InventoryItem item in allItems)
            {
                RemoveItem(item.Item);
            }
        }
    }
}