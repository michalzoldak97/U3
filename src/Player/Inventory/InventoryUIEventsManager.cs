using U3.Inventory;
using U3.Log;
using UnityEngine;
using UnityEngine.UIElements;

namespace U3.Player.Inventory
{
    public class InventoryUIEventsManager : MonoBehaviour
    {
        private PlayerInventoryMaster inventoryMaster;

        private void SetInit()
        {
            if (TryGetComponent(out PlayerInventoryMaster playerInventoryMaster))
            {
                inventoryMaster = playerInventoryMaster;
            }
            else
            {
                GameLogger.Log(new GameLog(
                Log.LogType.Error,
                    $"There is no PlayerInventoryMaster on the {transform.name} object, required by the {name}"));
            }
        }

        private void OnEnable()
        {
            if (inventoryMaster == null)
                SetInit();

            inventoryMaster.EventOnItemButtonDrop += OnItemButtonDrop;

            inventoryMaster.EventItemFocused += OnItemFocused;
            inventoryMaster.EventItemUnfocused += OnItemUnfocused;
            inventoryMaster.EventItemRemoved += OnItemUnfocused;
        }

        private void OnDisable()
        {
            inventoryMaster.EventOnItemButtonDrop -= OnItemButtonDrop;

            inventoryMaster.EventItemFocused -= OnItemFocused;
            inventoryMaster.EventItemUnfocused -= OnItemUnfocused;
            inventoryMaster.EventItemRemoved -= OnItemUnfocused;
        }

        private IInventoryDropArea GetButtonDropArea(RectTransform buttonTransform)
        {
            foreach (IInventoryDropArea inventoryDropArea in GetComponentsInChildren<IInventoryDropArea>())
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(
                    inventoryDropArea.DropAreaTransform, buttonTransform.position))
                    return inventoryDropArea;
            }

            return null;
        }

        private void OnItemButtonDrop(IItemButton itemButton, RectTransform buttonTransform)
        {
            IInventoryDropArea dropArea = GetButtonDropArea(buttonTransform);
            if (dropArea == null 
                || dropArea == itemButton.ParentArea
                || !dropArea.IsInventoryItemAccepted(itemButton.InventoryItem))
            {
                buttonTransform.SetParent(itemButton.ParentArea.ItemParentTransform);
                OnItemUnfocused(itemButton.InventoryItem.Item);
                return;
            }

            IInventoryDropArea prevArea = itemButton.ParentArea;

            itemButton.ChangeInventoryArea(dropArea);

            prevArea.ItemRemovedFromArea(itemButton.InventoryItem.Item);

            dropArea.AssignInventoryItem(itemButton.InventoryItem);

            OnItemUnfocused(itemButton.InventoryItem.Item);

            inventoryMaster.CallEventReloadBackpack();
        }

        private void ShutDownItemDetails()
        {
            foreach (Transform uiElement in inventoryMaster.DetailsParent.transform)
            {
                if (uiElement.TryGetComponent(out IItemDetails itemDetails))
                    Destroy(itemDetails.UIObject);
            }

            inventoryMaster.DetailsParent.SetActive(false);
        }

        private void SetUpItemDetails(Transform item)
        {
            InventoryItem inventoryItem = inventoryMaster.Items.GetItem(item);
            if (inventoryItem == null)
                return;

            inventoryMaster.DetailsParent.SetActive(true);
            ItemDetailsFactory.AddItemDetails(inventoryItem, inventoryMaster.DetailsParent.transform);
        }

        private void ToggleItemDetails(bool toActive, Transform item)
        {
            if (toActive)
                SetUpItemDetails(item);
            else
                ShutDownItemDetails();
        }

        private void OnItemFocused(Transform item)
        {
            inventoryMaster.FocusedItem = item;
            ToggleItemDetails(true, item);
        }

        private void OnItemUnfocused(Transform item)
        {
            if (item == inventoryMaster.FocusedItem)
                inventoryMaster.FocusedItem = null;

            if (inventoryMaster.FocusedItem == null)
                ToggleItemDetails(false, item);
        }
    }
}