using U3.Weapon.Explosive;
using U3.Core;
using U3.Item;
using U3.Destructible;
using UnityEngine;

namespace U3
{
    public class ExplosiveHEAT : Vassal<ExplosiveMaster>
    {
        private DamageImpactType impactType;
        private DamageElementType elementType;
        private float range;
        private Vector2 dmgEquation;
        private Vector2 penEquation;
        private Vector3 heatDir;
        private Transform m_Transform;

        public override void OnMasterEnabled(ExplosiveMaster master)
        {
            base.OnMasterEnabled(master);

            Master.EventExploded += ShootHEAT;
        }

        public void OnDisable()
        {
            Master.EventExploded -= ShootHEAT;
        }

        private void ShootHEAT(FireInputOrigin origin)
        {
           // should have own dmg inflictor setting
        }

        private void Start()
        {
            impactType = Master.DmgSettings.ImpactType;
            elementType = Master.DmgSettings.ElementType;
            dmgEquation = Master.DmgSettings.DamageEquation;
            penEquation = Master.DmgSettings.PenetrationEquation;
            heatDir = Master.DmgSettings.ExplosiveSetting.HEATDirection != Vector3.zero ?  Master.DmgSettings.ExplosiveSetting.HEATDirection : Vector3.zero;
            m_Transform = transform;
        }
    }
}
