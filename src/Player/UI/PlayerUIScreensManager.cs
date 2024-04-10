using System.Collections.Generic;
using U3.Global.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInputManager = U3.Input.PlayerInputManager;

namespace U3.Player.UI
{
    public class PlayerUIScreensManager : MonoBehaviour
    {
        private bool isMiniMapOn; // TODO: create mini map screen

        private readonly Dictionary<UIScreenType, IUIScreen> screens = new();

        private PlayerMaster playerMaster;

        private void OnEnable()
        {
            playerMaster = GetComponent<PlayerMaster>();

            PlayerInputManager.HumanoidInputActions.EventToggleInventory += ToggleInventory;
            PlayerInputManager.HumanoidInputActions.EventToggleMiniMap += ToggleMiniMap;

            PlayerInputManager.UIInputActions.EventToggleInventory += ToggleInventory;
            PlayerInputManager.UIInputActions.EventToggleMiniMap += ToggleMiniMap;
        }
        private void OnDisable()
        {
            PlayerInputManager.HumanoidInputActions.EventToggleInventory -= ToggleInventory;
            PlayerInputManager.HumanoidInputActions.EventToggleMiniMap -= ToggleMiniMap;

            PlayerInputManager.UIInputActions.EventToggleInventory -= ToggleInventory;
            PlayerInputManager.UIInputActions.EventToggleMiniMap -= ToggleMiniMap;
        }

        private void InformScreenDisabled(GameObject uiScreenObject)
        {
            foreach (IUIScreenStateDependent dependentUI in uiScreenObject.GetComponentsInChildren<IUIScreenStateDependent>())
            {
                dependentUI.OnUIScreenDisabled();
            }
        }

        private void DisableScreen(UIScreenType screenType)
        {
            InformScreenDisabled(screens[screenType].ScreenObj);
            screens[screenType].ScreenObj.SetActive(false);

            playerMaster.CallEventTogglePlayerControl(false, Controller.PlayerControlType.Cursor);
        }

        /// <summary>
        /// Call Disable method on all screens
        /// Deactivate screen objects
        /// Activate selected screen and call Enable on it
        /// </summary>
        /// <param name="screenType"></param>
        private void EnableScreen(UIScreenType screenType)
        {
            foreach (KeyValuePair<UIScreenType, IUIScreen> screen in screens)
            {
                DisableScreen(screen.Key);
            }

            screens[screenType].ScreenObj.SetActive(true);

            playerMaster.CallEventTogglePlayerControl(true, Controller.PlayerControlType.Cursor);
        }

        private void ToggleInventory()
        {
            if (screens[UIScreenType.Inventory].ScreenObj.activeSelf)
            {
                DisableScreen(UIScreenType.Inventory);

                playerMaster.CallEventTogglePlayerControl(true, Controller.PlayerControlType.Look);

                PlayerInputManager.ToggleActionMap(PlayerInputManager.PlayerInputActions.Humanoid);

                return;
            }

            playerMaster.CallEventTogglePlayerControl(false, Controller.PlayerControlType.Look);

            EnableScreen(UIScreenType.Inventory);

            PlayerInputManager.ToggleActionMap(PlayerInputManager.PlayerInputActions.UI);
        }

        private void ToggleMiniMap()
        {
            string camCode = isMiniMapOn ? "FPSPlayer" : "3rdCamera";
            InputActionMap inputActionsToSet = isMiniMapOn ? PlayerInputManager.PlayerInputActions.Humanoid : PlayerInputManager.PlayerInputActions.UI;

            SceneCameraManager.Instance.EnableSceneCamera(camCode);
            PlayerInputManager.ToggleActionMap(inputActionsToSet);
            isMiniMapOn = !isMiniMapOn;

            /*ApplicationState.IsSceneSwitching = true; TODO: remove dev code
            SceneManager.LoadSceneAsync(1);*/
        }

        private void FetchScreens()
        {
            foreach (Transform t in transform)
            {
                if (t.TryGetComponent(out IUIScreen uiScreen))
                {
                    uiScreen.ScreenObj = t.gameObject; // setting from outside because screen can be disabled on start
                    screens.Add(uiScreen.UIScreenType, uiScreen);
                }
            }
        }

        private void Start()
        {
            FetchScreens();
        }
    }
}
