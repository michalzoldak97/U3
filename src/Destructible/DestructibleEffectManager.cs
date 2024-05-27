using UnityEngine;

namespace U3.Destructible
{
    public class DestructibleEffectManager : MonoBehaviour
    {
        [SerializeField] private HitEffectSettings hitEffectSetttings;

        public static DestructibleEffectManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
    }
}
