using U3.Player.Controller;
using UnityEngine;

namespace U3.Player
{
    public class PlayerMaster : MonoBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private Transform fpsCamera;

        public bool IsActiveInventoryItemInteractionEnabled { get; set; }
        public PlayerSettings PlayerSettings { get { return playerSettings; } private set { } }

        public Transform FPSCamera { get { return fpsCamera; } private set { } }

        public delegate void PlayerControlTggleEventHandler(bool toActiveState, PlayerControlType controlType);
        public event PlayerControlTggleEventHandler EventTogglePlayerControl;

        public void CallEventTogglePlayerControl(bool toActiveState, PlayerControlType controlType)
        {
            EventTogglePlayerControl?.Invoke(toActiveState, controlType);
        }
    }
}
