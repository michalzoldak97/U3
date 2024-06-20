using U3.Core.Helper;
using U3.Item;
using UnityEngine;

namespace U3.Weapon.Explosive
{
    public class ExplosiveTargetFetcherNonAlloc : ExplosiveTargetFetcher
    {
        private Collider[] potentialTargets;

        protected override void FetchExplosionTargets(FireInputOrigin origin)
        {
            int potentialTargetsCount = Physics.OverlapSphereNonAlloc(m_Transform.position, radius, potentialTargets, origin.LayersToHit);
            if (potentialTargetsCount < 1)
                return;

            visibilityCheckData.checkLayers = origin.LayersToHit;
            visibilityCheckData.origin = m_Transform.position;

            for (int i = 0; i < potentialTargetsCount; i++)
            {
                (bool isVisible, Vector3 atPoint) = Helper.IsTransformVisibleFromPoint(potentialTargets[i], visibilityCheckData);
                if (isVisible)
                    Master.ExplosionTargets.Add((potentialTargets[i], atPoint));
            }
        }

        protected override void Start()
        {
            base.Start();
            potentialTargets = new Collider[Master.DmgSettings.ExplosiveSetting.TargetCapacity];
        }
    }
}