using UnityEngine;

namespace U3.Global.Config
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameConfigSettings", order = 0)]
    public class GameConfigSettings : ScriptableObject
    {
        public bool EnableLogs;
        public float DefaultDestructionDelay;
        public string InputActionCode;
    }
}
