using UnityEngine;
using UnityEngine.InputSystem;

namespace U3.Input
{
    public class HumanoidInputActions
    {
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
        public event HumanoidInputActionsEventHandler EventItemThrow;
        public event HumanoidInputActionsEventHandler EventToggleInventory;
        public event HumanoidInputActionsEventHandler EventAim;
        public event HumanoidInputActionsEventHandler EventShoot;
        public event HumanoidInputActionsEventHandler EventReload;
        public event HumanoidInputActionsEventHandler EventChangeWeaponMode;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot1;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot2;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot3;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot4;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot5;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot6;
        public event HumanoidInputActionsEventHandler EventChangeActiveInventorySlot7;

        public virtual void CallEventRunStart(InputAction.CallbackContext ctx) => EventRunStart?.Invoke();
        public virtual void CallEventRunFinish(InputAction.CallbackContext ctx) => EventRunFinish?.Invoke();
        public virtual void CallEventJump(InputAction.CallbackContext ctx) => EventJump?.Invoke();
        public virtual void CallEventItemInteract(InputAction.CallbackContext ctx) => EventItemInteract?.Invoke();
        public virtual void CallEventItemThrow(InputAction.CallbackContext ctx) => EventItemThrow?.Invoke();
        public virtual void CallEventToggleInventory(InputAction.CallbackContext ctx) => EventToggleInventory?.Invoke();
        public virtual void CallEventAim(InputAction.CallbackContext ctx) => EventAim?.Invoke();
        public virtual void CallEventShoot(InputAction.CallbackContext ctx) => EventShoot?.Invoke();
        public virtual void CallEventReload(InputAction.CallbackContext ctx) => EventReload?.Invoke();
        public virtual void CallEventChangeWeaponMode(InputAction.CallbackContext ctx) => EventChangeWeaponMode?.Invoke();
        public virtual void CallEventChangeActiveInventorySlot1(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot1?.Invoke();
        public virtual void CallEventChangeActiveInventorySlot2(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot2?.Invoke();
        public virtual void CallEventChangeActiveInventorySlot3(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot3?.Invoke();
        public virtual void CallEventChangeActiveInventorySlot4(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot4?.Invoke();
        public virtual void CallEventChangeActiveInventorySlot5(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot5?.Invoke();
        public virtual void CallEventChangeActiveInventorySlot6(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot6?.Invoke();
        public virtual void CallEventChangeActiveInventorySlot7(InputAction.CallbackContext ctx) => EventChangeActiveInventorySlot7?.Invoke();

        private PlayerInputActions playerInputActions;

        private void EnableActions()
        {
            playerInputActions.Humanoid.MouseX.Enable();
            playerInputActions.Humanoid.MouseY.Enable();
            playerInputActions.Humanoid.Move.Enable();
            playerInputActions.Humanoid.RunStart.Enable();
            playerInputActions.Humanoid.RunFinish.Enable();
            playerInputActions.Humanoid.Jump.Enable();
            playerInputActions.Humanoid.ItemInteract.Enable();
            playerInputActions.Humanoid.ItemThrow.Enable();
            playerInputActions.Humanoid.ToggleInventory.Enable();
            playerInputActions.Humanoid.Aim.Enable();
            playerInputActions.Humanoid.Shoot.Enable();
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
            playerInputActions.Humanoid.ItemThrow.performed += CallEventItemThrow;
            playerInputActions.Humanoid.ToggleInventory.performed += CallEventToggleInventory;
            playerInputActions.Humanoid.Aim.performed += CallEventAim;
            playerInputActions.Humanoid.Shoot.performed += CallEventShoot;
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
