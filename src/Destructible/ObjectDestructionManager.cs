using U3.Global.Config;
using UnityEngine;

namespace U3.Destructible
{
    public static class ObjectDestructionManager
    {
        public static void DestroyObject(GameObject obj)
        {
            obj.SetActive(false);
            Object.Destroy(obj, GameConfig.GameConfigSettings.DefaultDestructionDelay);
        }
    }
}