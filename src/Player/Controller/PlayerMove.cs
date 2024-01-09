using System.Collections;
using U3.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace U3.Player.Controller
{
    public class PlayerMove : MonoBehaviour
    {
        private bool isMoving;
        private int stateIdx = 0;
        private float gravityMultiplayer, inertiaCoeff;
        private Vector3 speedVec;
        private Vector3 upDir = Vector3.up;
        private Vector3 moveDir = Vector3.zero;
        private Transform m_Transform;
        private CharacterController m_CharacterController;
        private InputAction moveAction;
        private PlayerMoveManager moveManager;

        private void SetInit()
        {
            ControllerSettings controllerSettings = GetComponent<PlayerMaster>().PlayerSettings.Controller;
            gravityMultiplayer = controllerSettings.GravityMultiplayer;
            inertiaCoeff = controllerSettings.InertiaCoefficient;
            speedVec = new(controllerSettings.WalkSpeed, controllerSettings.RunSpeed, controllerSettings.JumpSpeed);

            m_Transform = transform;
            m_CharacterController = GetComponent<CharacterController>();
            moveAction = ActionMapManager.PlayerInputActions.Humanoid.Move;
            moveManager = GetComponent<PlayerMoveManager>();
        }

        private void OnEnable()
        {
            SetInit();

            moveAction.Enable();

            ActionMapManager.PlayerInputActions.Humanoid.Jump.performed += HandleJump;
            ActionMapManager.PlayerInputActions.Humanoid.Jump.Enable();

            ActionMapManager.PlayerInputActions.Humanoid.RunStart.performed += StartRun;
            ActionMapManager.PlayerInputActions.Humanoid.RunStart.Enable();

            ActionMapManager.PlayerInputActions.Humanoid.RunFinish.performed += StopRun;
            ActionMapManager.PlayerInputActions.Humanoid.RunFinish.Enable();
        }
        private void OnDisable()
        {
            moveAction.Disable();

            ActionMapManager.PlayerInputActions.Humanoid.Jump.performed -= HandleJump;
            ActionMapManager.PlayerInputActions.Humanoid.Jump.Disable();

            ActionMapManager.PlayerInputActions.Humanoid.RunStart.performed -= StartRun;
            ActionMapManager.PlayerInputActions.Humanoid.RunStart.Disable();

            ActionMapManager.PlayerInputActions.Humanoid.RunFinish.performed -= StopRun;
            ActionMapManager.PlayerInputActions.Humanoid.RunFinish.Disable();
        }

        private void StartRun(InputAction.CallbackContext obj)
        {
            stateIdx = 1;
        }

        private void StopRun(InputAction.CallbackContext obj)
        {
            stateIdx = 0;
        }

        private IEnumerator OverseeJumpState()
        {
            yield return new WaitForFixedUpdate();
            while (!m_CharacterController.isGrounded) yield return null;

            moveManager.CallEventLand(stateIdx);
        }
        private void HandleJump(InputAction.CallbackContext obj)
        {
            if (!m_CharacterController.isGrounded)
                return;

            moveDir.y = speedVec[2];
            moveManager.CallEventJump(stateIdx);
            StartCoroutine(OverseeJumpState());
        }

        private void CalcMoveVector(Vector2 moveInput)
        {
            bool areControlsPressed = !(moveInput.x == 0f && moveInput.y == 0f);

            if (areControlsPressed && m_CharacterController.isGrounded)
            {
                Vector3 mDir = m_Transform.forward * moveInput.y + m_Transform.right * moveInput.x;

                Physics.SphereCast(m_Transform.position, m_CharacterController.radius, -upDir, out RaycastHit hitInfo,
                                   m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
                mDir = Vector3.ProjectOnPlane(mDir, hitInfo.normal).normalized;
                moveDir.x = mDir.x * speedVec[stateIdx];
                moveDir.z = mDir.z * speedVec[stateIdx];

                moveManager.CallEventStep(stateIdx);
                isMoving = true;
            }
            else if (areControlsPressed)
            {
                moveDir.x *= inertiaCoeff;
                moveDir.z *= inertiaCoeff;
            }
            else if (isMoving)
            {
                moveDir.x = 0f;
                moveDir.z = 0f;

                moveManager.CallEventStoppedMoving(stateIdx);
                isMoving = false;
            }
        }

        private void MovePlayer(Vector2 moveInput)
        {
            CalcMoveVector(moveInput);

            m_CharacterController.Move(moveDir * Time.fixedDeltaTime);

            if (!m_CharacterController.isGrounded) // stick player to the ground
                moveDir += gravityMultiplayer * Time.fixedDeltaTime * Physics.gravity;
        }

        private void FixedUpdate()
        {
            MovePlayer(moveAction.ReadValue<Vector2>());
        }
    }
}
