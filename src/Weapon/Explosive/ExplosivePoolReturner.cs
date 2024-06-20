using System.Collections;
using U3.Destructible;
using U3.Item;
using U3.ObjectPool;
using UnityEngine;

namespace U3.Weapon.Explosive
{
    public class ExplosivePoolReturner : DamageInflictorReturner
    {
        private ExplosiveMaster explosiveMaster;

        private void OnEnable()
        {
            explosiveMaster = GetComponent<ExplosiveMaster>();
            explosiveMaster.EventExploded += StartReturnToPool;
        }

        private void OnDisable()
        {
            explosiveMaster.EventExploded -= StartReturnToPool;
        }

        private IEnumerator ReturnToPoolAtEnd()
        {
            yield return new WaitForEndOfFrame();

            ReturnToPool<DamageInflictor>();
        }

        private void StartReturnToPool(FireInputOrigin origin)
        {
            StartCoroutine(ReturnToPoolAtEnd());
        }
    }
}