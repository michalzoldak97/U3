using System;
using U3.Log;
using UnityEngine.InputSystem;

namespace U3.Input
{
    public static class ActionMapManager
    {
        public static HumanoidInputActions HumanoidInputActions => humanoidInputActions;

        public static PlayerInputActions PlayerInputActions => playerInputActions;
        public static event Action<InputActionMap> ActionMapChange;

        private static readonly PlayerInputActions playerInputActions = new();
        private static readonly HumanoidInputActions humanoidInputActions = new();

        public static void ToggleActionMap(InputActionMap actionMapToSet)
        {
            if (actionMapToSet.enabled)
            {
                GameLogger.Log(new GameLog(LogType.Warning, "action map is already active"));
                return;
            }

            playerInputActions.Disable();
            ActionMapChange?.Invoke(actionMapToSet);
            actionMapToSet.Enable();
        }

        public static void Init()
        {
            ToggleActionMap(playerInputActions.Humanoid);
            // create new humanoid actions, but allow to create test ones
            humanoidInputActions.SetInputActions(playerInputActions);
        }
    }
}
