using UnityEngine;

namespace U3.Core
{
    public class Vassal<T> : MonoBehaviour
    {
        protected T Master { get; private set; }

        public virtual void OnMasterEnabled(T master)
        {
            if (Master == null)
                Master = master;
        }

        public virtual void OnMasterDisabled() { }

    }
}
