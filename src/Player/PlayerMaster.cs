using U3.Player.Controller;
using UnityEngine;

namespace U3.Player
{
    public class PlayerMaster : MonoBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private Transform fpsCamera;

        public bool IsInventoryItemInteractionEnabled { get; set; }
        public PlayerSettings PlayerSettings => playerSettings;
        public Transform FPSCamera => fpsCamera;

        private readonly PlayerAPIMaster playerAPI = new();

        public delegate void PlayerControlTggleEventHandler(bool toActiveState, PlayerControlType controlType);
        public event PlayerControlTggleEventHandler EventTogglePlayerControl;

        public void CallEventTogglePlayerControl(bool toActiveState, PlayerControlType controlType)
        {
            EventTogglePlayerControl?.Invoke(toActiveState, controlType);
        }

        public void UpdateInventorySettings()
        {
            playerAPI.CallEventPlayerSettingsUpdate();
        }
    }
}
