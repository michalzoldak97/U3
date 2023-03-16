using U3.Input;
using U3.Item;
using U3.Log;
using UnityEngine;
using UnityEngine.InputSystem;
using LogType = U3.Log.LogType;

namespace U3.Player.Inventory
{
    public class PlayerItemDetector : MonoBehaviour
    {
        [SerializeField] private Transform fpsCamera;

        private bool isItemInRange;
        private int ignorePlayerLayerMask;
        private float nextCheck, checkRate;
        private Vector2 labelDimensions;
        private LayerMask itemLayer;
        private Transform itemInRange;
        private RaycastHit[] foundItemsBuffer;
        private Rect labelRect;
        private readonly GUIStyle labelStyle = new();

        private void SetInit()
        {
            InventorySettings inventorySettings = GetComponent<PlayerMaster>().PlayerSettings.Inventory;
            checkRate = inventorySettings.ItemCheckRate;
            labelDimensions = inventorySettings.LabelDimensions;
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

            foundItemsBuffer = new RaycastHit[1];
        }

        private void OnEnable()
        {
            SetInit();

            InputManager.PlayerInputActions.Humanoid.ItemInteract.performed += CallItemInteraction;
            InputManager.PlayerInputActions.Humanoid.ItemInteract.Enable();
        }

        private void OnDisable()
        {
            InputManager.PlayerInputActions.Humanoid.ItemInteract.performed -= CallItemInteraction;
            InputManager.PlayerInputActions.Humanoid.ItemInteract.Disable();
        }

        private void CallItemInteraction(InputAction.CallbackContext obj) // TODO check master cashing
        {
            if (!isItemInRange)
                return; 
            
            if (itemInRange.GetComponent<ItemMaster>() == null)
            {
                GameLogger.Log(LogType.Warning, "calling item interaction without item master");
                return;
            }

            itemInRange.GetComponent<ItemMaster>().CallEventInteractionCalled(transform);
        }

        private bool IsItemVisible()
        {
            if (Physics.Linecast(fpsCamera.position, foundItemsBuffer[0].transform.position, out RaycastHit hit, ignorePlayerLayerMask))
            {
                if (hit.transform == foundItemsBuffer[0].transform)
                    return true;
            }

            return false;
        }

        private void DetectItem()
        {
            int numItemsInRange = Physics.SphereCastNonAlloc(fpsCamera.position, .5f, fpsCamera.forward, foundItemsBuffer, 3f, itemLayer);

            if (numItemsInRange < 1)
            {
                isItemInRange = false;
                itemInRange = null;
                return;
            }

            Transform itemFound = foundItemsBuffer[0].transform;

            if (itemFound == itemInRange ||
                !IsItemVisible())
                return;

            isItemInRange = true;
            itemInRange = itemFound;
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
                GUI.Label(labelRect, itemInRange.name, labelStyle);
        }
    }
}
