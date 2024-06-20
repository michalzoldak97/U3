using UnityEngine;

namespace U3.Player.Controller
{
    public class PlayerMoveManager : MonoBehaviour
    {
        public delegate void MovementEventHandler(int state);
        public event MovementEventHandler EventStep;
        public event MovementEventHandler EventJump;
        public event MovementEventHandler EventLand;
        public event MovementEventHandler EventStoppedMoving;


        public delegate void PlayerControlMoveSpeedChangeEventHandler(Vector3 toSpeedVec);
        public event PlayerControlMoveSpeedChangeEventHandler EventChangeMoveSpeed;

        public void CallEventStep(int toStateIdx) => EventStep?.Invoke(toStateIdx);
        public void CallEventJump(int toStateIdx) => EventJump?.Invoke(toStateIdx);
        public void CallEventLand(int toStateIdx) => EventLand?.Invoke(toStateIdx);
        public void CallEventStoppedMoving(int toStateIdx) => EventStoppedMoving?.Invoke(toStateIdx);

        public void CallEventChangeMoveSpeed(Vector3 toSpeedVec) => EventChangeMoveSpeed?.Invoke(toSpeedVec);
    }
}
