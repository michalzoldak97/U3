using U3.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace U3.Player.Controller
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Transform fpsCamera;

        private float clampDeg, xRot;
        private Vector2 sensitivityXY;
        private Vector3 up;
        private Vector3 left;
        private Transform m_Transform;
        private InputAction lookX;
        private InputAction lookY;

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
        private void SetInit()
        {
            ControllerSettings controllerSettings = GetComponent<PlayerMaster>().PlayerSettings.controller;
            clampDeg = controllerSettings.lookClamp;
            sensitivityXY = controllerSettings.lookSensitivity;
            up = Vector3.up;
            left = Vector3.left;
            m_Transform = transform;
            lookX = InputManager.PlayerInputActions.Humanoid.MouseX;
            lookY = InputManager.PlayerInputActions.Humanoid.MouseY;
        }

        private void Start()
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

        private void Update()
        {
            Look();
        }
    }
}
