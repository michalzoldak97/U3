using U3.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace U3.Player.Controller
{
    public class PlayerLook : MonoBehaviour
    {
        private float clampDeg, xRot;
        private Vector2 sensitivityXY;
        private Vector3 up;
        private Vector3 left;
        private Transform m_Transform, fpsCamera;
        private InputAction lookX;
        private InputAction lookY;

        private void SetInit()
        {
            PlayerMaster playerMaster = GetComponent<PlayerMaster>();

            ControllerSettings controllerSettings = playerMaster.PlayerSettings.Controller;
            clampDeg = controllerSettings.LookClamp;
            sensitivityXY = controllerSettings.LookSensitivity;
            up = Vector3.up;
            left = Vector3.left;
            m_Transform = transform;
            fpsCamera = playerMaster.FPSCamera;
            lookX = ActionMapManager.PlayerInputActions.Humanoid.MouseX;
            lookY = ActionMapManager.PlayerInputActions.Humanoid.MouseY;
        }
        private void OnEnable()
        {
            SetInit();
            lookX.Enable();
            lookY.Enable();
        }
        private void OnDisable()
        {
            lookX.Disable();
            lookY.Disable();
        }
        private void Look()
        {
            float mouseX = lookX.ReadValue<float>();
            float mouseY = lookY.ReadValue<float>();

            if (mouseX != .0f)
                m_Transform.Rotate(up, mouseX * sensitivityXY.x);

            if (mouseY != .0f)
            {
                xRot += mouseY * sensitivityXY.y;
                xRot = Mathf.Clamp(xRot, -clampDeg, clampDeg);
                fpsCamera.localEulerAngles = left * xRot;
            }
        }
        private void Update()
        {
            Look();
        }
    }
}
