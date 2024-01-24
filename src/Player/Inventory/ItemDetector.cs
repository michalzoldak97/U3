using U3.Input;
using U3.Inventory;
using U3.Item;
using U3.Log;
using UnityEngine;
using UnityEngine.InputSystem;

namespace U3.Player.Inventory
{
    public class ItemDetector : MonoBehaviour
    {
        private bool isItemInRange;
        private bool isItemAvailable;
        private int ignorePlayerLayerMask;
        private float nextCheck, checkRate, searchRange, searchRadius;
        private string itemGUIText;
        private string inventoryFullMessageText;
        private Vector2 labelDimensions;
        private LayerMask itemLayer;
        private Transform itemInRange, fpsCamera;
        private RaycastHit[] foundItemsBuffer;
        private Rect labelRect;
        private readonly GUIStyle labelStyle = new();
        private InventoryMaster inventoryMaster;

        private void SetInit()
        {
            PlayerMaster playerMaster = GetComponent<PlayerMaster>();

            fpsCamera = playerMaster.FPSCamera;

            InventorySettings inventorySettings = playerMaster.PlayerSettings.Inventory;
            checkRate = inventorySettings.ItemCheckRate;
            searchRange = inventorySettings.ItemSearchRange;
            searchRadius = inventorySettings.ItemSearchRadius;
            labelDimensions = inventorySettings.LabelDimensions;
            inventoryFullMessageText = inventorySettings.InventoryFullMessageText;
            labelRect = new Rect
                (
                    Screen.width / 2f - labelDimensions.x,
                    Screen.height / 2f,
                    labelDimensions.x,
                    labelDimensions.y
                );
            labelStyle.fontSize = inventorySettings.LabelFontSize;
            labelStyle.normal.textColor = inventorySettings.LabelColor;

            itemLayer = 1 << LayerMask.NameToLayer("Item");

            // mask to exclude player layer from check for item visibility
            LayerMask playerLayer = 1 << LayerMask.NameToLayer("Player");
            ignorePlayerLayerMask = ~playerLayer;

            foundItemsBuffer = new RaycastHit[inventorySettings.ItemSearchBufferSize];

            inventoryMaster = GetComponent<InventoryMaster>();
        }

        private void OnEnable()
        {
            SetInit();

            ActionMapManager.PlayerInputActions.Humanoid.ItemInteract.performed += CallItemInteraction;
            ActionMapManager.PlayerInputActions.Humanoid.ItemInteract.Enable();

            inventoryMaster.EventItemRemoved += UpdateItemStatusOnInventoryChanged;
            inventoryMaster.EventInventoryCleared += UpdateItemStatusOnInventoryChanged;
        }

        private void OnDisable()
        {
            ActionMapManager.PlayerInputActions.Humanoid.ItemInteract.performed -= CallItemInteraction;
            ActionMapManager.PlayerInputActions.Humanoid.ItemInteract.Disable();

            inventoryMaster.EventItemRemoved -= UpdateItemStatusOnInventoryChanged;
            inventoryMaster.EventInventoryCleared -= UpdateItemStatusOnInventoryChanged;
        }

        private void CallItemInteraction(InputAction.CallbackContext obj)
        {
            if (!isItemInRange || !isItemAvailable)
                return;

            if (itemInRange.TryGetComponent(out ItemMaster itemMaster))
                itemMaster.CallEventInteractionCalled(transform);
            else
                GameLogger.Log(new GameLog(
                    Log.LogType.Warning,
                    $"calling item interaction on transform {itemInRange.name} without item master"));
        }

        private void UpdateItemStatus(Transform item)
        {
            if (item.TryGetComponent(out ItemMaster itemMaster))
            {
                if (!inventoryMaster.IsInventoryAvailableForItem(itemMaster.ItemSettings.ItemType))
                {
                    isItemAvailable = false;
                    itemGUIText = inventoryFullMessageText;
                    return;
                }

                itemGUIText = itemMaster.ItemSettings.GUITextToDisplay != "" ?
                    itemMaster.ItemSettings.GUITextToDisplay :
                    itemGUIText = item.name;
            }
            else
            {
                itemGUIText = item.name;
            }
        }

        private void UpdateItemStatusOnInventoryChanged(Transform item = null)
        {
            UpdateItemStatus(itemInRange);
        }

        private void UpdateItemStatusOnInventoryChanged()
        {
            UpdateItemStatus(itemInRange);
        }

        private bool IsItemVisible(Transform item)
        {
            if (Physics.Linecast(fpsCamera.position, item.position, out RaycastHit hit, ignorePlayerLayerMask))
            {
                if (hit.transform == item.transform)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Iterates over items buffer if not empty
        /// </summary>
        /// <returns>Visible item in the direction closest to the player camera</returns>
        private Transform GetBestItemFromBuffer()
        {
            float maxDot = 0f;
            Transform selected = null;

            for (int i = 0; i < foundItemsBuffer.Length; i++)
            {
                if (foundItemsBuffer[i].transform == null ||
                    !IsItemVisible(foundItemsBuffer[i].transform))
                    continue;

                float dot = Vector3.Dot(
                    fpsCamera.forward,
                    (foundItemsBuffer[i].transform.position - fpsCamera.position).normalized
                );

                if (dot < maxDot)
                    continue;

                maxDot = dot;
                selected = foundItemsBuffer[i].transform;
            }

            return selected;
        }

        private Transform DetectWithSphereCast()
        {
            int numItemsInRange = Physics.SphereCastNonAlloc(fpsCamera.position, searchRadius, fpsCamera.forward, foundItemsBuffer, searchRange, itemLayer);

            if (numItemsInRange < 1)
                return null;

            return GetBestItemFromBuffer();
        }

        /// <summary>
        /// If player points towards visible item it is assumed to be the most important
        /// </summary>
        /// <returns>Transform of the item that the player camera is pointing to</returns>
        private Transform DetectWithRaycast()
        {
            if (Physics.Raycast(fpsCamera.position, fpsCamera.forward, out RaycastHit hit, searchRange, itemLayer))
            {
                if (IsItemVisible(hit.transform))
                {
                    return hit.transform;
                }
            }

            return null;
        }

        private void DetectItem()
        { 
            Transform itemFound = DetectWithRaycast();

            if (itemFound == null)
                itemFound = DetectWithSphereCast();

            if (itemFound == null)
            {
                isItemInRange = false;
                itemInRange = null;
                return;
            }

            if (itemFound == itemInRange)
                return;

            isItemInRange = true;
            isItemAvailable = true;
            itemInRange = itemFound;

            UpdateItemStatus(itemFound);
        }

        private void ManageItemSearch()
        {
            if (!isItemInRange &&
                Time.time > nextCheck)
            {
                DetectItem();
                nextCheck = Time.time + checkRate;
            }
            else if (isItemInRange)
            {
                DetectItem();
            }
        }

        private void Update()
        {
            ManageItemSearch();
        }

        private void OnGUI()
        {
            if (isItemInRange)
                GUI.Label(labelRect, itemGUIText, labelStyle);
        }
    }
}
