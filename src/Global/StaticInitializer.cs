using U3.Input;
using UnityEngine;

namespace U3.Global.Config
{
    public class StaticInitializer : MonoBehaviour
    {
        private void Awake()
        {
            InputManager.Init();
            GameConfig.Init();
        }
    }
}
