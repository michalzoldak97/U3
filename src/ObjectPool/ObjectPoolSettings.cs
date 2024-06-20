using UnityEngine;

namespace U3.ObjectPool
{
    [System.Serializable]
    public class InstantiatingObjectPoolSetting
    {
        public bool AllowExpand;
        public int ExpandCount;
        public int ExpandCountLimit;
        public int OverheadBufferCount;
    }

    [System.Serializable]
    public class ObjectPoolSetting
    {
        public int StartSize;
        public ObjectPoolType Type;
        public string Code;
        public GameObject Object;
        public InstantiatingObjectPoolSetting InstantiatingPoolSetting;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectPoolSettings", order = 4)]
    public class ObjectPoolSettings : ScriptableObject
    {
        public ObjectPoolSetting[] ObjectPools;
    }
}
