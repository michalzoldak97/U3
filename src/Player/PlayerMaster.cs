using UnityEngine;

namespace U3.Player
{
    public class PlayerMaster : MonoBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private Transform fpsCamera;

        public PlayerSettings PlayerSettings { get { return playerSettings; } private set { } }

        public Transform FPSCamera { get { return fpsCamera; } private set { } }
    }
}
