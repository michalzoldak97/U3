using UnityEngine;

namespace U3.Player.Controller
{
    public class PlayerHeadBob : MonoBehaviour
    {
        private float bobTimer;
        private Vector2 cameraXYPos;
        private Vector2 headBobSpeed;
        private Vector2 headBobMagnitude;
        private Vector2 headBobMultiplayer;
        private Vector3 cameraPosToSet = Vector3.zero;
        private Transform fpsCamera;
        private PlayerMoveManager moveManager;
        private PlayerMaster playerMaster;

        private void SetInit()
        {
            playerMaster = GetComponent<PlayerMaster>();

            fpsCamera = playerMaster.FPSCamera;
            cameraXYPos = playerMaster.PlayerSettings.Controller.FPSCameraPos;

            ControllerSettings controllerSettings = playerMaster.PlayerSettings.Controller;
            headBobSpeed = controllerSettings.HeadBobSpeed;
            headBobMagnitude = controllerSettings.HeadBobMagnitude;
            headBobMultiplayer = controllerSettings.HeadBobMultiplayer;

            moveManager = GetComponent<PlayerMoveManager>();
        }

        private void OnEnable()
        {
            SetInit();

            moveManager.EventStep += HandleHeadBob;
            moveManager.EventStoppedMoving += ResetDefaultCameraPos;
        }

        private void OnDisable()
        {
            moveManager.EventStep -= HandleHeadBob;
            moveManager.EventStoppedMoving -= ResetDefaultCameraPos;
        }

        private void HandleHeadBob(int stateIdx)
        {
            bobTimer += Time.deltaTime * headBobSpeed[stateIdx];

            float headShift = Mathf.Sin(bobTimer) * headBobMagnitude[stateIdx];

            cameraPosToSet.x = fpsCamera.localPosition.x + (headShift * headBobMultiplayer[1]); 
            cameraPosToSet.y = cameraXYPos.y + (headShift * headBobMultiplayer[0]); 
            cameraPosToSet.z = fpsCamera.localPosition.z;

            fpsCamera.localPosition = cameraPosToSet;
        }

        private void ResetDefaultCameraPos(int _)
        {
            cameraPosToSet.x = playerMaster.PlayerSettings.Controller.FPSCameraPos.x;
            cameraPosToSet.y = playerMaster.PlayerSettings.Controller.FPSCameraPos.y;
            cameraPosToSet.z = fpsCamera.localPosition.z;

            fpsCamera.localPosition = cameraPosToSet;
        }
    }
}
