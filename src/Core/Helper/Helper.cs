using System.Collections.Generic;
using System.Reflection;
using U3.Log;
using UnityEngine;

namespace U3.Core.Helper
{
    public struct TransformVisibilityCheckData
    {

        public float range;
        public LayerMask checkLayers;
        public Vector3 origin;
        public (bool checkPos, bool checkCorners) checkPrecision;
    }

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

        public static (bool, Vector3) IsTransformVisibleFromPoint(Collider targetCol, TransformVisibilityCheckData checkData)
        {
            Transform target = targetCol.transform;
            RaycastHit hit;
            // Debug.DrawRay(checkData.origin, (targetCol.ClosestPoint(checkData.origin) - checkData.origin).normalized * checkData.range, Color.green, 10);

            if (Physics.Raycast(checkData.origin, (targetCol.ClosestPoint(checkData.origin) - checkData.origin).normalized, out hit, checkData.range, checkData.checkLayers))
            {
                if (hit.transform == target)
                    return (true, hit.point);
            }

            // Debug.DrawRay(checkData.origin, (target.position - checkData.origin).normalized * checkData.range, Color.red, 10);
            if (checkData.checkPrecision.checkPos &&
               Physics.Raycast(checkData.origin, (target.position - checkData.origin).normalized, out hit, checkData.range, checkData.checkLayers))
            {
                if (hit.transform == target)
                    return (true, hit.point);
            }

            if (!checkData.checkPrecision.checkCorners)
                return (false, Vector3.zero);

            LayerMask checkLayers = checkData.checkLayers;
            float range = checkData.range;
            Vector3 oPos = checkData.origin;
            Vector3 targetCenter = target.position;
            Vector3 cornerPos = targetCenter;
            Vector3 targetBounds = targetCol.bounds.extents * .99f; // to move ray a bit to the center

            cornerPos.y += targetBounds.y;
            cornerPos += target.forward * targetBounds.z;
            // Debug.DrawRay(oPos, (cornerPos - oPos).normalized * range, Color.blue, 10);
            if (Physics.Raycast(oPos, (cornerPos - oPos).normalized, out hit, range, checkLayers))
            {
                if (hit.transform == target)
                    return (true, hit.point);
            }

            // centre rear
            cornerPos = targetCenter - (target.forward * targetBounds.z);
            // Debug.DrawRay(oPos, (cornerPos - oPos).normalized * range, Color.blue, 10);
            if (Physics.Raycast(oPos, (cornerPos - oPos).normalized, out hit, range, checkLayers))
            {
                if (hit.transform == target)
                    return (true, hit.point);
            }

            // center +x
            cornerPos = targetCenter + (target.right * targetBounds.x);
            // Debug.DrawRay(oPos, (cornerPos - oPos).normalized * range, Color.blue, 10);
            if (Physics.Raycast(oPos, (cornerPos - oPos).normalized, out hit, range, checkLayers))
            {
                if (hit.transform == target)
                    return (true, hit.point);
            }

            // center -x
            cornerPos = targetCenter - (target.right * targetBounds.x);
            // Debug.DrawRay(oPos, (cornerPos - oPos).normalized * range, Color.blue, 10);
            if (Physics.Raycast(oPos, (cornerPos - oPos).normalized, out hit, range, checkLayers))
            {
                if (hit.transform == target)
                    return (true, hit.point);
            }

            return (false, Vector3.zero);
        }
    }
}