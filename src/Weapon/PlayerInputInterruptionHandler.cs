using U3.Item;
using UnityEngine.InputSystem;

namespace U3.Weapon
{
    public class PlayerInputInterruptionHandler : ItemInputInterruption
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            Input.PlayerInputManager.ActionMapChange += (InputActionMap actionMap) => inputProvider.CallEventInputInterrupted();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Input.PlayerInputManager.ActionMapChange -= (InputActionMap actionMap) => inputProvider.CallEventInputInterrupted();
        }
    }
}
