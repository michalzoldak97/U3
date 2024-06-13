using U3.Weapon.Explosive;
using U3.Core;
using U3.Item;
using U3.Destructible;
using UnityEngine;

namespace U3
{
    public class ExplosiveSplinter : Vassal<ExplosiveMaster>
    {
        private int splinterNum;
        private DamageImpactType impactType;
        private DamageElementType elementType;
        private float radius;
        private Vector2 dmgEquation;
        private Vector2 penEquation;
        private Transform m_Transform;

        public override void OnMasterEnabled(ExplosiveMaster master)
        {
            base.OnMasterEnabled(master);

            Master.EventExploded += ShootSplinters;
        }

        public void OnDisable()
        {
            Master.EventExploded -= ShootSplinters;
        }

        private void ShootSplinters(FireInputOrigin origin)
        {
            for (int i = 0; i < splinterNum; i++)
            {
                // Vector3 dir = Random.insideUnitSphere.normalized; Debug.DrawRay(m_Transform.position, dir * radius, Color.red, 20f);

                if (Physics.Raycast(
                        m_Transform.position,
                        Random.insideUnitSphere.normalized,
                        out RaycastHit hit,
                        radius,
                        origin.LayersToDamage
                ))
                {
                    ObjectDamageManager.InflictDamage(hit.transform, new DamageData()
                    {
                        InflictorID = origin.ID,
                        ImpactType = impactType,
                        ElementType = elementType,
                        RealDamage = Random.Range(dmgEquation.x - dmgEquation.x * dmgEquation.y, dmgEquation.x + dmgEquation.x * dmgEquation.y),
                        RealPenetration = Random.Range(penEquation.x - penEquation.x * penEquation.y, penEquation.x + penEquation.x * penEquation.y)
                    });
                }
            }
        }

        private void Start()
        {
            splinterNum = Master.DmgSettings.ExplosiveSetting.SplinterNum;
            impactType = Master.DmgSettings.ImpactType;
            elementType = Master.DmgSettings.ElementType;
            radius = Master.DmgSettings.ExplosiveSetting.Radius;
            dmgEquation = Master.DmgSettings.DamageEquation;
            penEquation = Master.DmgSettings.PenetrationEquation;
            m_Transform = transform;
        }
    }
}
