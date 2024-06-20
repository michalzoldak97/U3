using System.Collections.Generic;
using U3.Log;
using UnityEngine;

namespace U3.Damageable
{
    public static class ObjectDamageManager
    {
        private static readonly Dictionary<Transform, IDamageReciever> damagableObjects = new();
        private static readonly Dictionary<int, DamageData> damageInflicted = new();

        public static void RegisterDamagable(Transform objTransform, IDamageReciever dmgReciever)
        {
            if (damagableObjects.ContainsKey(objTransform))
            {
                GameLogger.Log(new GameLog(Log.LogType.Warning, $"trying to add already existing dmg reciever {objTransform}"));
                return;
            }

            damagableObjects.Add(objTransform, dmgReciever);
        }

        public static void UnregisterDamagable(Transform objTransform)
        {
            if (!damagableObjects.ContainsKey(objTransform))
                return; // no log because preferring ContainsKey over TryGetComponent for pooled objects

            damagableObjects.Remove(objTransform);
        }

        public static void InflictDamage(Transform objTransform, DamageData dmgData)
        {
            if (!damagableObjects.ContainsKey(objTransform))
            {
                // GameLogger.Log(new GameLog(Log.LogType.Warning, $"trying to damage not existing dmg reciever {objTransform}"));
                return;
            }

            Debug.Log($"object {objTransform} is damaged with dmg {dmgData.RealDamage} and penetration {dmgData.RealPenetration}");
            damagableObjects[objTransform].CallEventReceiveDamage(dmgData);
        }

        public static void RegisterDamage(int instanceID, DamageData dmgData)
        {
            if (damageInflicted.ContainsKey(instanceID))
            {
                DamageData newDmg = damageInflicted[instanceID];
                newDmg.RealDamage += dmgData.RealDamage;
                damageInflicted[instanceID] = newDmg;
            }
            else
                damageInflicted[instanceID] = dmgData;
        }
    }
}
