using U3.Input;
using UnityEngine;

namespace U3.Global
{
    public class StaticInitializer : MonoBehaviour
    {
        public void Initialize()
        {
            PlayerInputManager.Init();
        }
    }
}
