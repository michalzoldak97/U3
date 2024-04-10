using UnityEngine;

namespace U3.Global
{
    public class Vassal<T> : MonoBehaviour
    {
        protected T Master { get; private set; }

        public virtual void OnMasterEnabled(T master)
        {
            Master ??= master;
        }

        public virtual void OnMasterDisabled() { }
    }
}
