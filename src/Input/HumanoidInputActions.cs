using UnityEngine;
using UnityEngine.InputSystem;

namespace U3.Input
{
    public class HumanoidInputActions
    {
        private PlayerInputActions playerInputActions;
        public virtual void SetMouseX(float toSet) { }
        public virtual float MouseX { 
            get 
            { 
                return playerInputActions.Humanoid.MouseX.ReadValue<float>(); 
            }
            protected set { } 
        }

        public virtual void SetMouseY(float toSet) { }
        public virtual float MouseY
        {
            get
            {
                return playerInputActions.Humanoid.MouseY.ReadValue<float>();
            }
            protected set { }
        }

        public virtual void SetMove(Vector2 toSet) { }
        public virtual Vector2 Move
        {
            get
            {
                return playerInputActions.Humanoid.Move.ReadValue<Vector2>();
            }
            protected set { }
        }

        public delegate void HumanoidInputActionsEventHandler();
        public event HumanoidInputActionsEventHandler EventRunStart;
        public event HumanoidInputActionsEventHandler EventRunFinish;
        public event HumanoidInputActionsEventHandler EventJump;
        public event HumanoidInputActionsEventHandler EventItemInteract;
        public event HumanoidInputActionsEventHandler EventToggleInventory;
        public event HumanoidInputActionsEventHandler EventToggleMiniMap;
        public event HumanoidInputActionsEventHandler EventAimDown;
        public event HumanoidInputActionsEventHandler EventAimUp;
        public event HumanoidInputActionsEventHandler EventFireDown;
        public event HumanoidInputActionsEventHandler EventFireUp;
        public event HumanoidInputActionsEventHandler EventReload;
        public event HumanoidInputActionsEventHandler EventChangeWeaponMode;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot1;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot2;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot3;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot4;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot5;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot6;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot7;

        public void CallEventRunStart(InputAction.CallbackContext ctx) => EventRunStart?.Invoke();
        public void CallEventRunFinish(InputAction.CallbackContext ctx) => EventRunFinish?.Invoke();
        public void CallEventJump(InputAction.CallbackContext ctx) => EventJump?.Invoke();
        public void CallEventItemInteract(InputAction.CallbackContext ctx) => EventItemInteract?.Invoke();
        public void CallEventToggleInventory(InputAction.CallbackContext ctx) => EventToggleInventory?.Invoke();
        public void CallEventToggleMiniMap(InputAction.CallbackContext ctx) => EventToggleMiniMap?.Invoke();
        public void CallEventAimDown(InputAction.CallbackContext ctx) => EventAimDown?.Invoke();
        public void CallEventAimUp(InputAction.CallbackContext ctx) => EventAimUp?.Invoke();
        public void CallEventFireDown(InputAction.CallbackContext ctx) => EventFireDown?.Invoke();
        public void CallEventFireUp(InputAction.CallbackContext ctx) => EventFireUp?.Invoke();
        public void CallEventReload(InputAction.CallbackContext ctx) => EventReload?.Invoke();
        public void CallEventChangeWeaponMode(InputAction.CallbackContext ctx) => EventChangeWeaponMode?.Invoke();
        public void CallEventChangeActiveInventorySlot1(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot1?.Invoke();
        public void CallEventChangeActiveInventorySlot2(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot2?.Invoke();
        public void CallEventChangeActiveInventorySlot3(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot3?.Invoke();
        public void CallEventChangeActiveInventorySlot4(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot4?.Invoke();
        public void CallEventChangeActiveInventorySlot5(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot5?.Invoke();
        public void CallEventChangeActiveInventorySlot6(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot6?.Invoke();
        public void CallEventChangeActiveInventorySlot7(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot7?.Invoke();

        private void EnableActions()
        {
            playerInputActions.Humanoid.MouseX.Enable();
            playerInputActions.Humanoid.MouseY.Enable();
            playerInputActions.Humanoid.Move.Enable();
            playerInputActions.Humanoid.RunStart.Enable();
            playerInputActions.Humanoid.RunFinish.Enable();
            playerInputActions.Humanoid.Jump.Enable();
            playerInputActions.Humanoid.ItemInteract.Enable();
            playerInputActions.Humanoid.ToggleInventory.Enable();
            playerInputActions.Humanoid.ToggleMiniMap.Enable();
            playerInputActions.Humanoid.AimDown.Enable();
            playerInputActions.Humanoid.AimUp.Enable();
            playerInputActions.Humanoid.FireDown.Enable();
            playerInputActions.Humanoid.FireUp.Enable();
            playerInputActions.Humanoid.Reload.Enable();
            playerInputActions.Humanoid.ChangeWeaponMode.Enable();
            playerInputActions.Humanoid.ChangeActiveInventorySlot1.Enable();
            playerInputActions.Humanoid.ChangeActiveInventorySlot2.Enable();
            playerInputActions.Humanoid.ChangeActiveInventorySlot3.Enable();
            playerInputActions.Humanoid.ChangeActiveInventorySlot4.Enable();
            playerInputActions.Humanoid.ChangeActiveInventorySlot5.Enable();
            playerInputActions.Humanoid.ChangeActiveInventorySlot6.Enable();
            playerInputActions.Humanoid.ChangeActiveInventorySlot7.Enable();
        }

        private void SubscribeActions()
        {
            playerInputActions.Humanoid.RunStart.performed += CallEventRunStart;
            playerInputActions.Humanoid.RunFinish.performed += CallEventRunFinish;
            playerInputActions.Humanoid.Jump.performed += CallEventJump;
            playerInputActions.Humanoid.ItemInteract.performed += CallEventItemInteract;
            playerInputActions.Humanoid.ToggleInventory.performed += CallEventToggleInventory;
            playerInputActions.Humanoid.ToggleMiniMap.performed += CallEventToggleMiniMap;
            playerInputActions.Humanoid.AimDown.performed += CallEventAimDown;
            playerInputActions.Humanoid.AimUp.performed += CallEventAimUp;
            playerInputActions.Humanoid.FireDown.performed += CallEventFireDown;
            playerInputActions.Humanoid.FireUp.performed += CallEventFireUp;
            playerInputActions.Humanoid.Reload.performed += CallEventReload;
            playerInputActions.Humanoid.ChangeWeaponMode.performed += CallEventChangeWeaponMode;
            playerInputActions.Humanoid.ChangeActiveInventorySlot1.performed += CallEventChangeActiveInventorySlot1;
            playerInputActions.Humanoid.ChangeActiveInventorySlot2.performed += CallEventChangeActiveInventorySlot2;
            playerInputActions.Humanoid.ChangeActiveInventorySlot3.performed += CallEventChangeActiveInventorySlot3;
            playerInputActions.Humanoid.ChangeActiveInventorySlot4.performed += CallEventChangeActiveInventorySlot4;
            playerInputActions.Humanoid.ChangeActiveInventorySlot5.performed += CallEventChangeActiveInventorySlot5;
            playerInputActions.Humanoid.ChangeActiveInventorySlot6.performed += CallEventChangeActiveInventorySlot6;
            playerInputActions.Humanoid.ChangeActiveInventorySlot7.performed += CallEventChangeActiveInventorySlot7;
        }

        public void SetInputActions(PlayerInputActions playerInputActions)
        {
            this.playerInputActions = playerInputActions;
            EnableActions();
            SubscribeActions();
        }
    }
}
