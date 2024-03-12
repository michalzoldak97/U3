using UnityEngine;

namespace U3.Input
{
    public class PlayableHumanoidInputActions : HumanoidInputActions
    {
        public void SetMouseX(float toSet)
        {
            MouseX = toSet;
        }
        public override float MouseX { get; protected set; }

        public void SetMouseY(float toSet)
        {
            MouseY = toSet;
        }
        public override float MouseY { get; protected set; }

        public void SetMove(Vector2 toSet)
        {
            Move = toSet;
        }
        public override Vector2 Move { get; protected set; }
    }
}
