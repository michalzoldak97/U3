using System;
using U3.Log;
using UnityEngine.InputSystem;

namespace U3.Input
{
    public static class InputManager
    {
        public static PlayerInputActions PlayerInputActions { get; private set; }
        public static event Action<InputActionMap> ActionMapChange;

        public static void ToggleActionMap(InputActionMap actionMapToSet)
        {
            if (actionMapToSet.enabled)
            {
                GameLogger.Log(LogType.Warning, "action map is already active");
                return;
            }

            PlayerInputActions.Disable();
            ActionMapChange?.Invoke(actionMapToSet);
            actionMapToSet.Enable();
        }

        public static void Init()
        {
            PlayerInputActions = new();
            ToggleActionMap(PlayerInputActions.Humanoid);
        }
    }
}
