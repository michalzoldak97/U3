using UnityEngine;
using UnityEngine.InputSystem;

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
        private Rect labelRect;
        private GUIStyle labelStyle = new();

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
        }

        private void OnEnable()
        {
            SetInit();

            InputManager.playerInputActions.ItemInteract.performed += CallItemInteraction;
            InputManager.playerInputActions.ItemInteract.Enable();
        }

        private void OnDisable()
        {
            InputManager.playerInputActions.ItemInteract.performed -= CallItemInteraction;
            InputManager.playerInputActions.ItemInteract.Disable();
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

        private void OnGUI()
        {
            if (isItemInRange)
                GUI.Label(labelRect. itemInRange.name, labelStyle);
        }
    }
}
