using System.Collections.Generic;
using U3.Log;
using UnityEngine;

namespace U3.Destructible
{

    public static class ObjectDamageManager
    {
        private static readonly Dictionary<Transform, IDamageReciever> damagableObjects = new();
        private static readonly Dictionary<int, DamageData> damageInflictors = new();
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

        public static void RegisterDamageInflictor(int instanceID, DamageData dmgData)
        {
            if (damageInflictors.ContainsKey(instanceID))
            {
                GameLogger.Log(new GameLog(Log.LogType.Warning, $"trying to register existing dmg inflictor {instanceID}"));
                return;
            }

            damageInflictors[instanceID] = dmgData;
        }

        public static void UnregisterDamageInflictor(int instanceID)
        {
            if (!damageInflictors.ContainsKey(instanceID))
            {
                GameLogger.Log(new GameLog(Log.LogType.Warning, $"trying to unregister not existing dmg inflictor {instanceID}"));
                return;
            }

            damageInflictors.Remove(instanceID);
        }

        public static void UpdateDamageInflictorOrigin(int instanceID, int originInstanceID)
        {
            if (!damageInflictors.ContainsKey(instanceID))
            {
                GameLogger.Log(new GameLog(Log.LogType.Warning, $"trying to access not existing dmg inflictor {instanceID}"));
                return;
            }

            DamageData inflictorDmgData = damageInflictors[instanceID];
            inflictorDmgData.InflictorID = originInstanceID;

            damageInflictors[instanceID] = inflictorDmgData;
        }

        public static DamageData GetDamageInflictorData(int instanceID)
        {
            if (!damageInflictors.ContainsKey(instanceID))
            {
                GameLogger.Log(new GameLog(Log.LogType.Error, $"trying to access not existing dmg inflictor {instanceID}"));
                return new DamageData();
            }

            return damageInflictors[instanceID];
        }

        public static void InflictDamage(Transform objTransform, DamageData dmgData)
        {
            if (!damagableObjects.ContainsKey(objTransform))
            {
                GameLogger.Log(new GameLog(Log.LogType.Warning, $"trying to damage not existing dmg reciever {objTransform}"));
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
