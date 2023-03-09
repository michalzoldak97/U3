using UnityEngine;

namespace U3.Player
{
    [System.Serializable]
    public struct ControllerSettings
    {
        public float walkSpeed;
        public float runSpeed;
        public float jumpSpeed;
        public float stickToGroundForce;
        public float gravityMultiplayer;
        public float inertiaCoefficient;
        public float lookClamp;
        public Vector2 lookSensitivity;
        public Vector2 stepSpeed;
        public Vector2 headBobSpeed;
        public Vector2 headBobMagnitude;
        public Vector2 headBobMultiplayer;
    }
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerSettings", order = 0)]
    public class PlayerSettings : ScriptableObject
    {
        public ControllerSettings controller;
    }
}
