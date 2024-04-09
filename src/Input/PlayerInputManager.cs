using System;
using U3.Global.Config;
using UnityEngine.InputSystem;

namespace U3.Input
{
    public static class PlayerInputManager
    {
        public static HumanoidInputActions HumanoidInputActions => humanoidInputActions;
        public static UIInputActions UIInputActions => uIInputActions;

        public static PlayerInputActions PlayerInputActions => playerInputActions;
        public static event Action<InputActionMap> ActionMapChange;

        private static readonly PlayerInputActions playerInputActions = new();
        private readonly static HumanoidInputActions humanoidInputActions = HumanoidInputActionsFactory.GetInputActions(GameConfig.GameConfigSettings.InputActionCode);
        private readonly static UIInputActions uIInputActions = new();

        public static void ToggleActionMap(InputActionMap actionMapToSet)
        {
            playerInputActions.Disable();
            ActionMapChange?.Invoke(actionMapToSet);
            actionMapToSet.Enable();
        }

        public static void Init()
        {
            ToggleActionMap(playerInputActions.Humanoid);
            humanoidInputActions.SetInputActions(playerInputActions);
            uIInputActions.SetInputActions(playerInputActions);
        }
    }
}
