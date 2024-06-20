using UnityEngine.InputSystem;

namespace U3.Input
{
    public class UIInputActions
    {
        private PlayerInputActions playerInputActions;

        public delegate void UIInputActionsEventHandler();
        public event UIInputActionsEventHandler EventToggleInventory;
        public event UIInputActionsEventHandler EventToggleMiniMap;
        public event UIInputActionsEventHandler EventItemThrow;

        public void CallEventToggleInventory(InputAction.CallbackContext ctx) => EventToggleInventory?.Invoke();
        public void CallEventToggleMiniMap(InputAction.CallbackContext ctx) => EventToggleMiniMap?.Invoke();
        public void CallEventItemThrow(InputAction.CallbackContext ctx) => EventItemThrow?.Invoke();

        private void EnableActions()
        {
            playerInputActions.UI.ToggleInventory.Enable();
            playerInputActions.UI.ToggleMiniMap.Enable();
            playerInputActions.UI.ItemThrow.Enable();
        }

        private void SubscribeActions()
        {
            playerInputActions.UI.ToggleInventory.performed += CallEventToggleInventory;
            playerInputActions.UI.ToggleMiniMap.performed += CallEventToggleMiniMap;
            playerInputActions.UI.ItemThrow.performed += CallEventItemThrow;
        }

        public void SetInputActions(PlayerInputActions playerInputActions)
        {
            this.playerInputActions = playerInputActions;
            EnableActions();
            SubscribeActions();
        }
    }
}
