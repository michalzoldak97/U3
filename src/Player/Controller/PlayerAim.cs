using U3.Inventory;
using U3.Item;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace U3.Player.Controller
{
    public class PlayerAim : MonoBehaviour
    {
        private bool isAiming;
        private Vector2 cameraXYPos;
        private Transform fpsCameraTransform;
        private Camera fpsCamera;

        private PlayerMaster playerMaster;
        private InventoryMaster inventoryMaster;
        private PlayerMoveManager moveManager;

        private void Setinit()
        {
            inventoryMaster = GetComponent<InventoryMaster>();

            playerMaster = GetComponent<PlayerMaster>();
            fpsCameraTransform = playerMaster.FPSCamera;
            fpsCamera = fpsCameraTransform.GetComponent<Camera>();
            cameraXYPos = new Vector2(fpsCameraTransform.localPosition.x, fpsCameraTransform.localPosition.y);

            moveManager = GetComponent<PlayerMoveManager>();
        }

        private void OnEnable()
        {
            Setinit();

            Input.PlayerInputManager.HumanoidInputActions.EventAimDown += OnAimAttempt;
            Input.PlayerInputManager.HumanoidInputActions.EventAimUp += ResetFPSCameraPosition;
            Input.PlayerInputManager.ActionMapChange += ResetFPSCameraPosition;
            SceneManager.sceneLoaded += ResetFPSCameraPosition;
            inventoryMaster.EventItemDeselected += ResetFPSCameraPosition;
        }

        private void OnDisable()
        {
            Input.PlayerInputManager.HumanoidInputActions.EventAimDown -= OnAimAttempt;
            Input.PlayerInputManager.HumanoidInputActions.EventAimUp -= ResetFPSCameraPosition;
            Input.PlayerInputManager.ActionMapChange -= ResetFPSCameraPosition;
            SceneManager.sceneLoaded -= ResetFPSCameraPosition;
            inventoryMaster.EventItemDeselected -= ResetFPSCameraPosition;
        }

        private void StopAim()
        {
            if (!isAiming)
                return;

            fpsCamera.fieldOfView = playerMaster.PlayerSettings.Controller.FPSCameraFOV;
            fpsCameraTransform.localPosition = cameraXYPos;
            ControllerSettings cs = playerMaster.PlayerSettings.Controller;
            moveManager.CallEventChangeMoveSpeed(new Vector3(cs.WalkSpeed, cs.RunSpeed, cs.JumpSpeed));
            playerMaster.CallEventTogglePlayerControl(true, PlayerControlType.HeadBob);
            isAiming = false;
        }

        private void ResetFPSCameraPosition() => StopAim();
        private void ResetFPSCameraPosition(InputActionMap _) => StopAim();
        private void ResetFPSCameraPosition(Scene s, LoadSceneMode l) => StopAim();
        private void ResetFPSCameraPosition(Transform _) => StopAim();

        private void OnAimAttempt()
        {
            if (!isAiming &&
                inventoryMaster.SelectedItem != null &&
                inventoryMaster.SelectedItem.TryGetComponent(out IAimable aimingItem))
            {
                playerMaster.CallEventTogglePlayerControl(false, PlayerControlType.HeadBob);
                ControllerSettings cs = playerMaster.PlayerSettings.Controller;
                moveManager.CallEventChangeMoveSpeed(new Vector3(cs.WalkSpeed, cs.WalkSpeed, cs.JumpSpeed));
                fpsCameraTransform.localPosition = aimingItem.ItemParentAimPosition;
                fpsCamera.fieldOfView = aimingItem.ItemParentAimFOV;
                isAiming = true;
            }
        }
    }
}
