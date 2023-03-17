using U3.Inventory;
using U3.Item;
using U3.Log;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        private Transform fpsCamera;

        private InventoryItem currentItem;
        private InventoryMaster inventoryMaster;

        private void SetInit()
        {
            fpsCamera = GetComponent<PlayerMaster>().FPSCamera;
            inventoryMaster = GetComponent<InventoryMaster>();
        }

        private void OnEnable()
        {
            SetInit();

            inventoryMaster.EventAddItem += AddItem;
        }

        private void OnDisable()
        {
            inventoryMaster.EventAddItem -= AddItem;
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
        /// Sets inventory item to selected or deselected state on the inventory
        /// If the current item is the deselected one it's set to null
        /// </summary>
        /// <param name="toState"></param>
        /// <param name="itemTransform"></param>
        private void ToggleItem(bool toState, Transform itemTransform)
        {
            InventoryItem item = inventoryMaster.Items[itemTransform];

            item.Object.SetActive(toState);
            item.IsSelected = toState;

            if (toState)
            {
                item.ItemMaster.CallEventSelected();
                currentItem = item;
            }
            else
            {
                item.ItemMaster.CallEventDeselected();
                if (currentItem == item)
                    currentItem = null;
            }
        }

        /// <summary>
        /// Handles item collider and rigidbody when placing on player
        /// If theres no item selected the picked up item will be selected
        /// Then the item is placed on the parent transform
        /// </summary>
        /// <param name="itemTransform"></param>
        private void SetItemState(Transform itemTransform)
        {
            InventoryItem item = inventoryMaster.Items[itemTransform];

            ToggleItemPhysics(false, item);

            if (currentItem == null)
                ToggleItem(true, itemTransform);
            else if (!item.ItemMaster.ItemSettings.KeepObjActive)
                ToggleItem(false, itemTransform);

            itemTransform.SetParent(fpsCamera);
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

            FetchRigidbodies(item);
            FetchColliders(item);

            inventoryMaster.Items.Add(item, newItem);
            SetItemState(item);

            inventoryMaster.CallEventItemAdded();
        }
    }
}
