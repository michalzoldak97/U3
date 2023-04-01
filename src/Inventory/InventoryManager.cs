using System.Collections.Generic;
using U3.Item;
using U3.Log;
using UnityEngine;

namespace U3.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        protected Transform itemContainer;

        protected InventoryItem currentItem;
        protected InventoryMaster inventoryMaster;

        protected virtual void SetInit() {}
        private void Awake()
        {
            inventoryMaster = GetComponent<InventoryMaster>();
            inventoryMaster.Items = new();
            inventoryMaster.EventAddItem += AddItem;
        }
        protected virtual void OnEnable()
        {
            SetInit();
            inventoryMaster.EventRemoveItem += RemoveItem;
            inventoryMaster.EventSelectItem += SelectItem;
            inventoryMaster.EventDeselectItem += DeselectItem;
        }

        protected virtual void OnDisable()
        {
            inventoryMaster.EventAddItem -= AddItem;
            inventoryMaster.EventRemoveItem -= RemoveItem;
            inventoryMaster.EventSelectItem -= SelectItem;
            inventoryMaster.EventDeselectItem -= DeselectItem;
        }

        private void DeselectItem(InventoryItem item)
        {
            item.IsSelected = false;

            if (currentItem == item)
                currentItem = null;

            item.ItemMaster.CallEventDeselected();

            item.Object.SetActive(false);

            inventoryMaster.CallEventItemDeselected(item.Object.transform);
        }

        private void SelectItem(InventoryItem item)
        {
            item.Object.SetActive(true);
            item.IsSelected = true;

            currentItem = item;

            item.ItemMaster.CallEventSelected();
            inventoryMaster.CallEventItemSelected(item.Object.transform);
        }

        /// <summary>
        /// Sets inventory item to selected or deselected state on the inventory
        /// If the current item is the deselected one it's set to null
        /// </summary>
        /// <param name="toState"></param>
        /// <param name="itemTransform"></param>
        private void ToggleItem(bool toState, Transform itemTransform)
        {
            InventoryItem item = inventoryMaster.Items[itemTransform];

            if (toState)
            {
                SelectItem(item);
                return;
            }

            if (item.IsSelected)
            {
                DeselectItem(item);
                return;
            }

            item.Object.SetActive(false);
            item.IsSelected = false;
        }

        private void SelectItem(Transform item)
        {
            ToggleItem(true, item);
        }

        private void DeselectItem(Transform item)
        {
            ToggleItem(false, item);
        }

        /// <summary>
        /// Sets objec physics so it can be stored on parent
        /// </summary>
        /// <param name="toState"></param>
        /// <param name="item"></param>
        private void ToggleItemPhysics(bool toState, InventoryItem item)
        {
            foreach (Rigidbody rb in item.RBs)
            {
                rb.isKinematic = !toState;
                rb.useGravity = toState;
            }

            if (item.ItemMaster.ItemSettings.KeepColliderActive)
                return;

            foreach (Collider col in item.Colliders)
            {
                col.isTrigger = !toState;
            }
        }

        /// <summary>
        /// Handles item collider and rigidbody when placing on player
        /// If theres no item selected the picked up item will be selected
        /// Then the item is placed on the parent transform
        /// </summary>
        /// <param name="activateForInventory"></param>
        /// <param name="itemTransform"></param>
        private void SetItemState(bool activateForInventory, Transform itemTransform)
        {
            InventoryItem item = inventoryMaster.Items[itemTransform];

            ToggleItemPhysics(!activateForInventory, item);

            if (activateForInventory)
            {
                itemTransform.SetParent(itemContainer);
                item.ItemMaster.CallEventAddedToInventory(); // inform item it has been added
            }
            else
            {
                itemTransform.SetParent(null);
                item.ItemMaster.CallEventRemovedFromInventory(); // inform item it has been removed
            }

            if (!activateForInventory)
                return;

            if (!item.ItemMaster.ItemSettings.KeepObjActive)
                ToggleItem(false, itemTransform);
        }

        /// <summary>
        /// Activates the item so it can be removed
        /// Enables item physics, sets parent to null and calls throw request on item
        /// </summary>
        /// <param name="itemTransform"></param>
        private void RemoveItem(Transform itemTransform)
        {
            InventoryItem item = inventoryMaster.Items[itemTransform];

            item.Object.SetActive(true);
            SetItemState(false, itemTransform);

            item.ItemMaster.CallEventThrow(itemContainer);
            inventoryMaster.Items.Remove(itemTransform);

            inventoryMaster.CallEventItemRemoved();
        }

        private void FetchColliders(InventoryItem item)
        {
            List<Collider> colliders = new();
            foreach (Collider col in item.Object.GetComponents<Collider>())
            {
                colliders.Add(col);
            }

            foreach (Collider col in item.Object.GetComponentsInChildren<Collider>())
            {
                colliders.Add(col);
            }

            item.Colliders = colliders.ToArray();
        }

        private void FetchRigidbodies(InventoryItem item)
        {
            List<Rigidbody> rigidbodies = new();
            foreach (Rigidbody rb in item.Object.GetComponents<Rigidbody>())
            {
                rigidbodies.Add(rb);
            }

            foreach (Rigidbody rb in item.Object.GetComponentsInChildren<Rigidbody>())
            {
                rigidbodies.Add(rb);
            }

            item.RBs = rigidbodies.ToArray();
        }

        /// <summary>
        /// Creates inventory item object and adds it to inventory dictionary
        /// Item state is set and event on inventory called
        /// </summary>
        /// <param name="item"></param>
        private void AddItem(Transform item)
        {
            if (inventoryMaster.Items.ContainsKey(item))
            {
                GameLogger.Log(Log.LogType.Warning, "trying to add already existing item to the inventory");
                return;
            }

            InventoryItem newItem = new()
            {
                ItemMaster = item.GetComponent<ItemMaster>(),
                Object = item.gameObject
            };

            if (newItem.ItemMaster == null)
            {
                GameLogger.Log(Log.LogType.Error, "add item called by transform without master");
                return;
            }

            newItem.Type = newItem.ItemMaster.ItemSettings.ItemType;

            FetchRigidbodies(newItem);
            FetchColliders(newItem);

            inventoryMaster.Items.Add(item, newItem); // add to all inventory items

            SetItemState(true, item); // disable item on parent

            inventoryMaster.CallEventItemAdded();
        }
    }
}
