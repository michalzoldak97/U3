using U3.Item;
using UnityEngine;
using UnityEngine.InputSystem;

namespace U3.Weapon
{
    public class PlayerInputInterruptionHandler : ItemInputInterruption
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            Debug.Log("Subscribing");

            Input.PlayerInputManager.ActionMapChange += (InputActionMap actionMap) => CallItemInterruption();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Debug.Log("Unsubscribing");

            Input.PlayerInputManager.ActionMapChange -= (InputActionMap actionMap) => CallItemInterruption();
        }
    }
}
