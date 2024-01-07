using System.Collections.Generic;
using UnityEngine;

namespace U3.Global.Helper
{
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
    }
}