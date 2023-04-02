using System.Collections.Generic;
using UnityEngine;

namespace U3.Player.Controller
{
    public class PlayerControlsToggler : MonoBehaviour
    {
        private readonly Dictionary<PlayerControlType, MonoBehaviour> playerControlScripts = new();
        private PlayerMaster playerMaster;

        private void OnEnable()
        {
            playerMaster = GetComponent<PlayerMaster>();

            playerMaster.EventTogglePlayerControl += TogglePlayerControl;
        }
        private void OnDisable()
        {
            playerMaster.EventTogglePlayerControl -= TogglePlayerControl;
        }

        private void TogglePlayerControl(bool toActiveState, PlayerControlType controlType)
        {
            if (!playerControlScripts.ContainsKey(controlType) ||
                playerControlScripts[controlType].enabled == toActiveState)
                return;

            playerControlScripts[controlType].enabled = toActiveState;
        }

        private void Start()
        {
            playerControlScripts.Add(PlayerControlType.Move, GetComponent<PlayerMove>());
            playerControlScripts.Add(PlayerControlType.Look, GetComponent<PlayerLook>());
            playerControlScripts.Add(PlayerControlType.HeadBob, GetComponent<PlayerHeadBob>());
        }
    }
}
