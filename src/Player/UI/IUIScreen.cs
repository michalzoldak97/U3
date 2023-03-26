using UnityEngine;

namespace U3.Player.UI
{
    public interface IUIScreen
    {
        public UIScreenType UIScreenType { get; }
        public GameObject ScreenObj { get; set; }

        public void Enable();
        public void Disable();
    }
}
