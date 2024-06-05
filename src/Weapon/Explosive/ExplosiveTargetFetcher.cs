using U3.Core;
using U3.Core.Helper;
using U3.Item;
using UnityEngine;

namespace U3.Weapon.Explosive
{
    public class ExplosiveTargetFetcher : Vassal<ExplosiveMaster>
    {
        protected (bool, bool) visibilityCheckPrecision;
        protected float radius;
        protected Transform m_Transform;
        protected TransformVisibilityCheckData visibilityCheckData;

        public override void OnMasterEnabled(ExplosiveMaster master)
        {
            base.OnMasterEnabled(master);

            Master.EventFetchTargets += FetchExplosionTargets;
        }

        private void OnDisable()
        {
            Master.EventFetchTargets -= FetchExplosionTargets;
        }

        protected virtual void FetchExplosionTargets(FireInputOrigin origin)
        {
            
        }

        protected virtual void Start()
        {
            visibilityCheckPrecision = (Master.DmgSettings.ExplosiveSetting.CheckClosestPoint, Master.DmgSettings.ExplosiveSetting.CheckCorners);
            radius = Master.DmgSettings.ExplosiveSetting.Radius;
            m_Transform = transform;
            visibilityCheckData = new TransformVisibilityCheckData() { range = radius, checkPrecision = visibilityCheckPrecision };
        }
    }
}