using System.Collections.Generic;
using UnityEngine;

namespace U3.Engine
{
    public static class Engine
    {
        public static T[] FetchAllComponents<T>(GameObject obj) where T : Component //TODO: generic fetch
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