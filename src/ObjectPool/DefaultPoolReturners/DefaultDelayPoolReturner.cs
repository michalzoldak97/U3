using System.Collections;
using UnityEngine;

namespace U3.ObjectPool.DefaultPoolReturners
{
    public class DefaultDelayPoolReturner : ReturnToObjectPool
    {
        private const float timeToReturn = 2.0f;

        private readonly WaitForSeconds waitDefault = new(timeToReturn);

        private IEnumerator DelayReturn()
        {
            yield return waitDefault;
            ReturnToPool();
        }

        private void OnEnable()
        {
            StartCoroutine(DelayReturn());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}
