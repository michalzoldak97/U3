using System.Collections.Generic;
using U3.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace U3.Player.UI
{
    public class PlayerUIScreensManager : MonoBehaviour
    {
        private readonly Dictionary<UIScreenType, IUIScreen> screens = new();

        PlayerMaster playerMaster;

        // TODO: handle cursor and PlayerLook

        private void OnEnable()
        {
            playerMaster = GetComponent<PlayerMaster>();

            InputManager.PlayerInputActions.Humanoid.ToggleInventory.performed += ToggleInventory;
            InputManager.PlayerInputActions.Humanoid.ToggleInventory.Enable();
        }
        private void OnDisable()
        {
            InputManager.PlayerInputActions.Humanoid.ToggleInventory.performed -= ToggleInventory;
            InputManager.PlayerInputActions.Humanoid.ToggleInventory.Disable();
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
                screen.Value.Disable();
                screen.Value.ScreenObj.SetActive(false);
            }

            screens[screenType].ScreenObj.SetActive(true);
            screens[screenType].Enable();
        }
        private void ToggleInventory(InputAction.CallbackContext obj)
        {
            if (screens[UIScreenType.Inventory].ScreenObj.activeSelf)
            {
                screens[UIScreenType.Inventory].Disable();
                screens[UIScreenType.Inventory].ScreenObj.SetActive(false);

                playerMaster.CallEventTogglePlayerControl(true, Controller.PlayerControlType.Look);

                return;
            }

            playerMaster.CallEventTogglePlayerControl(false, Controller.PlayerControlType.Look);

            EnableScreen(UIScreenType.Inventory);
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
