using U3.Player.Controller;
using UnityEngine;

namespace U3.Player
{
    public class PlayerCursorToggler : MonoBehaviour
    {
        private PlayerMaster playerMaster;

        private void OnEnable()
        {
            playerMaster = GetComponent<PlayerMaster>();

            playerMaster.EventTogglePlayerControl += ToggleCursor;
        }

        private void OnDisable()
        {
            playerMaster.EventTogglePlayerControl -= ToggleCursor;
        }

        private void ToggleCursor(bool toActiveState, PlayerControlType controlType)
        {
            if (controlType != PlayerControlType.Cursor)
                return;

            if (toActiveState && 
                Cursor.lockState != CursorLockMode.None)
            {
                playerMaster.IsInventoryItemInteractionEnabled = false; // disable mouse click events when UI is active
                Cursor.lockState = CursorLockMode.None;
            }
            else if (!toActiveState &&
                Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                playerMaster.IsInventoryItemInteractionEnabled = true;
            }
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerMaster.IsInventoryItemInteractionEnabled = true;
        }
    }
}
