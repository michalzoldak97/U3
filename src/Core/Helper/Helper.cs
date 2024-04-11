using System.Collections.Generic;
using System.Reflection;
using U3.Log;
using UnityEngine;

namespace U3.Core.Helper
{
    [System.Serializable]
    internal class JSONWrapper<T>
    {
        public T[] Items;
    }
    public static class Helper
    {
        public static T[] FetchAllComponents<T>(GameObject obj) where T : Component
        {
            List<T> components = new();
            foreach (T c in obj.GetComponents<T>())
            {
                components.Add(c);
            }

            foreach (T c in obj.GetComponentsInChildren<T>())
            {
                components.Add(c);
            }

            return components.ToArray();
        }

        private static bool CanEvaluatePropertyUniqueness(int arrLen, string typeName, string propertyName, PropertyInfo propertyInfo)
        {
            if (arrLen < 1)
            {
                GameLogger.Log(new GameLog(
                Log.LogType.Error,
                        $"Attempt to check for uniqueness of the {propertyName} propoerty on the empty array on {typeName} objects"));
                return false;
            }

            if (propertyInfo == null)
            {
                GameLogger.Log(new GameLog(
                Log.LogType.Error,
                        $"The type {typeName} does not have a property named {propertyName} it has info: "));
                return false;
            }

            return true;
        }

        public static bool IsPropertyUnique<T>(T[] arr, string propertyName) where T : class
        {
            var propertyValues = new HashSet<object>();
            var propertyInfo = typeof(T).GetProperty(propertyName);


            if (!CanEvaluatePropertyUniqueness(arr.Length, typeof(T).Name, propertyName, propertyInfo))
                return false;

            foreach (var obj in arr)
            {
                var value = propertyInfo.GetValue(obj);
                if (propertyValues.Contains(value))
                {
                    return false;
                }
                propertyValues.Add(value);
            }
            return true;
        }

        public static string ArrayToJSON<T>(T[] arr) => JsonUtility.ToJson(new JSONWrapper<T> { Items = arr });

        public static T[] JSONToArray<T>(string json) => JsonUtility.FromJson<JSONWrapper<T>>(json).Items;
    }
}