using U3.Input;
using UnityEngine;

namespace U3.Global
{
    public class StaticInitializer : MonoBehaviour
    {
        private void Awake()
        {
            PlayerInputManager.Init();
            Config.GameConfig.Init();
        }
    }
}
