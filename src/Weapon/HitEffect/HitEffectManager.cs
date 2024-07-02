using System.Collections.Generic;
using U3.ObjectPool;
using UnityEngine;

namespace U3.Weapon.Effect
{
    public class HitEffectManager : MonoBehaviour
    {
        [SerializeField] private HitEffectSettings hitEffectSetttings;

        private readonly Dictionary<string, HitEffectSetting> hitEffectConfig = new();
        private ObjectPoolsManager effectPool;

        public static HitEffectManager Instance { get; private set; }
        
        private Vector3 GetScale(Vector3 scale)
        {
            return scale == Vector3.zero ? Vector3.one : scale;
        }

        public void PlayEffect(Vector3 effectPos, string effectCode, Vector3 effectScale = new Vector3())
        {
            PooledObject<HitEffect> effect = effectPool.GetEffect(effectCode);
            effect.Obj.SetActive(true);
            effect.Obj.transform.position = effectPos;
            effect.ObjTransform.localScale = GetScale(effectScale);
            effect.ObjInterface.Play();
        }

        private void PlayEffect(Vector3 hitPoint, Vector3 hitNormal, string effectCode, Vector3 effectScale)
        {
            PooledObject<HitEffect> effect = effectPool.GetEffect(effectCode);
            effect.Obj.SetActive(true);
            if (effect.ObjInterface.IsLocked)
                return;


            effect.ObjTransform.SetPositionAndRotation(hitPoint, Quaternion.LookRotation(-hitNormal));
            effect.ObjTransform.localScale = GetScale(effectScale);
            effect.ObjInterface.Play();
        }

        public void FireHitEffect(int hitLayer, Vector3 hitPoint, Vector3 hitNormal, string effectSettingCode, Vector3 effectScale = new Vector3())
        {
            HitEffectSetting effectSetting = hitEffectConfig[effectSettingCode];
            for (int i = 0; i < effectSetting.HitLayers.Length; i++)
            {
                if ((effectSetting.HitLayers[i].value & (1 << hitLayer)) != 0)
                {
                    PlayEffect(hitPoint, hitNormal, effectSetting.EffectCodes[i], effectScale);
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
