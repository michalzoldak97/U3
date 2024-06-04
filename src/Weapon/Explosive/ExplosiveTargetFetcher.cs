using U3.Core;
using U3.Item;
using UnityEngine;

namespace U3.Weapon.Explosive
{
    public class ExplosiveTargetFetcher : Vassal<ExplosiveMaster>
    {
        public override void OnMasterEnabled(ExplosiveMaster master)
        {
            base.OnMasterEnabled(master);

            Master.EventFetchTargets += OnFetchEvent;
        }

        private void OnDisable()
        {
            Master.EventFetchTargets -= OnFetchEvent;
        }

        protected virtual Collider[] FetchExplosionTargets(LayerMask layersToHit)
        {
            return new Collider[0];
        }

        private void OnFetchEvent(FireInputOrigin origin)
        {
            Collider[] targets = FetchExplosionTargets(origin.LayersToHit);

            for (int i = 0; i < targets.Length; i++)
            {
                Master.ExplosionTargets.Add(targets[i]);
            }
        }
    }
}