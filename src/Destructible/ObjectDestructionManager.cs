using U3.Global.Config;
using UnityEngine;

namespace U3.Destructible
{
    public static class ObjectDestructionManager
    {
        public static void DestroyObject(GameObject obj)
        {
            ObjectDamageManager.UnregisterDamagable(obj.transform);
            ObjectDamageManager.UnregisterDamageInflictor(obj.GetInstanceID());
            
            obj.SetActive(false);
            Object.Destroy(obj, GameConfig.GameConfigSettings.DefaultDestructionDelay);
        }
    }
}
