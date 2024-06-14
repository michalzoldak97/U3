using U3.Weapon.Explosive;
using U3.Core;
using U3.Item;
using U3.Destructible;
using UnityEngine;

namespace U3
{
    public class ExplosiveHEAT : Vassal<ExplosiveMaster>
    {
        private Vector3 heatDir;

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
            Transform m_Transform = transform;
            Vector3 dir = heatDir == Vector3.zero ? m_Transform.forward : heatDir * m_Transform.rotation; // shoot ray forward if no setting, otherwise shoot setting dir relative to warhead
            Debug.DrawRay(m_Transform.position, dir * range, Color.black, 20f);
            if (Physics.Raycast(
                    m_Transform.position,
                    dir,
                    out RaycastHit hit,
                    Master.DmgSettings.ExplosiveSetting.HEATDamageSettings.FireRange;,
                    origin.LayersToDamage
            ))
            {
                Vector2 dmgEquation = Master.DmgSettings.ExplosiveSetting.HEATDamageSettings.DamageEquation;
                Vector2 penEquation = Master.DmgSettings.ExplosiveSetting.HEATDamageSettings.PenetrationEquation;

                ObjectDamageManager.InflictDamage(hit.transform, new DamageData()
                {
                    InflictorID = origin.ID,
                    ImpactType = Master.DmgSettings.ExplosiveSetting.HEATDamageSettings.ImpactType;,
                    ElementType = Master.DmgSettings.ExplosiveSetting.HEATDamageSettings.ElementType;,
                    RealDamage = Random.Range(dmgEquation.x - dmgEquation.x * dmgEquation.y, dmgEquation.x + dmgEquation.x * dmgEquation.y),
                    RealPenetration = Random.Range(penEquation.x - penEquation.x * penEquation.y, penEquation.x + penEquation.x * penEquation.y)
                });
            }
        }

        private void Start()
        {
            heatDir = Master.DmgSettings.ExplosiveSetting.HEATDirection != Vector3.zero ?  Master.DmgSettings.ExplosiveSetting.HEATDirection : Vector3.zero;
        }
    }
}
