﻿using System.Collections;
using U3.Damageable;
using U3.Log;
using U3.Weapon.Effect;
using UnityEngine;

namespace U3.ObjectPool
{
    public class DelayReturner : MonoBehaviour
    {
        [SerializeField] private ObjectPoolType poolType;
        [SerializeField] private float delaySec;

        private WaitForSeconds waitDelay;
        private PooledObjectReturner objectReturner;

        private IEnumerator ReturnToPool()
        {
            yield return waitDelay;

            switch (poolType)
            {
                case ObjectPoolType.DamageInflictor:
                    objectReturner.ReturnToPool<DamageInflictor>();
                    break;
                case ObjectPoolType.Effect:
                    objectReturner.ReturnToPool<HitEffect>();
                    break;
                case ObjectPoolType.DamageText:
                    objectReturner.ReturnToPool<DamageText>();
                    break;
                default:
                    break;
            }
        }

        private void Start() 
        {
            waitDelay = new WaitForSeconds(delaySec);

            if (TryGetComponent(out PooledObjectReturner por))
            {
                objectReturner = por;
                StartCoroutine(ReturnToPool());
            }
            else
            {
                GameLogger.Log(new GameLog(
                        Log.LogType.Error,
                        $"Missing PooledObjectReturner on {gameObject.name} object"));
            }
        }
    }
}