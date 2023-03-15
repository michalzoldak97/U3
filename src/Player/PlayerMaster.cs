using UnityEngine;

namespace U3.Player
{
    public class PlayerMaster : MonoBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;

        public PlayerSettings PlayerSettings { get { return playerSettings; } private set { } }
    }
}
