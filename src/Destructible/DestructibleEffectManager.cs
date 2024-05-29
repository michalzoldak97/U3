using System.Collections.Generic;
using U3.ObjectPool;
using UnityEngine;

namespace U3.Destructible
{
    public class DestructibleEffectManager : MonoBehaviour
    {
        [SerializeField] private HitEffectSettings hitEffectSetttings;

        private readonly Dictionary<string, HitEffectSetting> hitEffectConfig = new();
        private ObjectPoolsManager effectPool;

        public static DestructibleEffectManager Instance;

        private void PlayEffect(Vector3 hitPoint, Vector3 hitNormal, string effectCode)
        {
            PooledObject<Effect> effect = effectPool.GetEffect(effectCode);
            effect.Obj.SetActive(true);
            if (effect.ObjInterface.IsLocked)
                return;

            effect.ObjTransform.SetPositionAndRotation(hitPoint, Quaternion.LookRotation(-hitNormal));
            effect.ObjInterface.Play();
        }

        public void FireHitEffect(int hitLayer, Vector3 hitPoint, Vector3 hitNormal, string effectSettingCode)
        {
            HitEffectSetting effectSetting = hitEffectConfig[effectSettingCode];
            for (int i = 0; i < effectSetting.HitLayers.Length; i++)
            {
                if ((effectSetting.HitLayers[i].value & (1 << hitLayer)) != 0)
                {
                    PlayEffect(hitPoint, hitNormal, effectSetting.EffectCodes[i]);
                    break;
                }
            }
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void BuildHitEffectConfig()
        {
            HitEffectSetting[] hitConfig = hitEffectSetttings.HitEffectConfig;
            for (int i = 0; i < hitConfig.Length; i++)
            {
                hitEffectConfig[hitConfig[i].HitEffectCode] = hitConfig[i];
            }
        }

        private void Start()
        {
            effectPool = ObjectPoolsManager.Instance;
            BuildHitEffectConfig();
        }
    }
}
