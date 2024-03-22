using UnityEngine;

namespace U3.Global.Helper
{
  public class ObjectStateProxy : MonoBehaviour
  {
     private bool isChangeByMethod = true;

     public virtual void ChangeObjectState(toState bool)
     {
       isChangeByMethod = true;
       gameObject.SetActive(toState);
     }

     private void CheckIsChangeByMethod()
     {
       if (!isChangeByMethod)
       {
          GameLogger.Log(new GameLog(LogType.Warning, "action map is already active"));
          return;
       }
       isChangeByMethod = false;
     }
     
     protected virtual void OnEnable()
     {
       CheckIsChangeByMethod();
     }

     protected virtual OnDisable()
     {
       CheckIsChangeByMethod();
     }
  }
}
