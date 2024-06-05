using U3.Core.Helper;
using U3.Item;
using UnityEngine;

namespace U3.Weapon.Explosive
{
    public class ExplosiveTargetFetcherAlloc : ExplosiveTargetFetcher
    {
        protected override void FetchExplosionTargets(FireInputOrigin origin)
        {
            visibilityCheckData.checkLayers = origin.LayersToHit;
            visibilityCheckData.origin = m_Transform.position;

            Collider[] potentialTargets = Physics.OverlapSphere(m_Transform.position, radius, origin.LayersToHit);

            for (int i = 0; i < potentialTargets.Length; i++)
            {
                (bool isVisible, Vector3 atPoint) target = Helper.IsTransformVisibleFromPoint(potentialTargets[i].transform, visibilityCheckData);
                if (target.isVisible)
                    Master.ExplosionTargets.Add((potentialTargets[i], target.atPoint));
            }
        }
    }
}