using U3.Log;
using UnityEngine;

namespace U3.Global.GameScene
{
    public class ObjectStateProxy : MonoBehaviour
    {
        protected bool isChangeByMethod = true;

        public virtual void ChangeObjectState(bool toState)
        {
            isChangeByMethod = true;
            gameObject.SetActive(toState);
        }

        private void CheckIsChangeByMethod()
        {
            if (!isChangeByMethod)
            {
                GameLogger.Log(new GameLog(Log.LogType.Error,
                    $"Attempt to modify state of the {gameObject.name} outside of the state proxy"));
                return;
            }
            isChangeByMethod = false;
        }
     
        protected virtual void OnEnable()
        {
            CheckIsChangeByMethod();
        }

        protected virtual void OnDisable()
        {
            if (ApplicationState.IsSceneSwitching || ApplicationState.IsQuitting)
                return;

            CheckIsChangeByMethod();
        }
    }
}
