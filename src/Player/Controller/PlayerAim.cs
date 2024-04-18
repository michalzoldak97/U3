using U3.Inventory;
using U3.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace U3.Player.Controller
{
    public class PlayerAim : MonoBehaviour
    {
        private bool isAiming;
        private Vector2 cameraXYPos;
        private Vector3 headAimPosition;
        private Transform fpsCamera;

        private PlayerMaster playerMaster;
        private InventoryMaster inventoryMaster;

        private void Setinit()
        {
            inventoryMaster = GetComponent<InventoryMaster>();

            playerMaster = GetComponent<PlayerMaster>();
            headAimPosition = playerMaster.PlayerSettings.Controller.HeadAimPosition;
            fpsCamera = playerMaster.FPSCamera;
            cameraXYPos = new Vector2(fpsCamera.localPosition.x, fpsCamera.localPosition.y);
        }

        private void OnEnable()
        {
            Setinit();

            Input.PlayerInputManager.HumanoidInputActions.EventAimDown += OnAimAttempt;
            Input.PlayerInputManager.HumanoidInputActions.EventAimUp += ResetFPSCameraPosition;
            Input.PlayerInputManager.ActionMapChange += ResetFPSCameraPosition;
            SceneManager.sceneLoaded += ResetFPSCameraPosition;
        }

        private void OnDisable()
        {
            Input.PlayerInputManager.HumanoidInputActions.EventAimDown -= OnAimAttempt;
            Input.PlayerInputManager.HumanoidInputActions.EventAimUp -= ResetFPSCameraPosition;
            Input.PlayerInputManager.ActionMapChange -= ResetFPSCameraPosition;
            SceneManager.sceneLoaded -= ResetFPSCameraPosition;
        }

        private void StopAim()
        {
            if (!isAiming)
                return;

            fpsCamera.localPosition = cameraXYPos;
            playerMaster.CallEventTogglePlayerControl(true, PlayerControlType.HeadBob);
            isAiming = false;
        }

        private void ResetFPSCameraPosition() => StopAim();
        private void ResetFPSCameraPosition(InputActionMap _) => StopAim();
        private void ResetFPSCameraPosition(Scene s, LoadSceneMode l) => StopAim();

        private void OnAimAttempt()
        {
            if (inventoryMaster.SelectedItem != null &&
                inventoryMaster.SelectedItem.TryGetComponent(out WeaponAim _))
            {
                playerMaster.CallEventTogglePlayerControl(false, PlayerControlType.HeadBob);
                fpsCamera.localPosition = headAimPosition;
                isAiming = true;
            }
            else
                return;
        }
    }
}
