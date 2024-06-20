using U3.Global.Config;
using U3.Damageable;
using UnityEngine;

namespace U3.Destructible
{
    public static class ObjectDestructionManager
    {
        public static void DestroyObject(GameObject obj)
        {
            ObjectDamageManager.UnregisterDamagable(obj.transform);
            
            obj.SetActive(false);
            Object.Destroy(obj, GameConfig.GameConfigSettings.DefaultDestructionDelay);
        }
    }
}
