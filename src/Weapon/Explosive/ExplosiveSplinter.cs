using U3.Core;
using U3.Item;
using U3.Damageable;
using UnityEngine;

namespace U3.Weapon.Explosive
{
    public class ExplosiveSplinter : Vassal<ExplosiveMaster>
    {
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
            int splinterNum = Master.DmgSettings.ExplosiveSetting.SplinterNum;
            DamageElementType elementType = Master.DmgSettings.ElementType;
            float radius = Master.DmgSettings.ExplosiveSetting.Radius;
            Vector2 dmgEquation = Master.DmgSettings.DamageEquation;
            Vector2 penEquation = Master.DmgSettings.PenetrationEquation;
            Transform m_Transform = transform;

            for (int i = 0; i < splinterNum; i++)
            {
                Vector3 dir = Random.insideUnitSphere.normalized; Debug.DrawRay(m_Transform.position, dir * radius, Color.red, 20f);

                if (Physics.Raycast(
                        m_Transform.position,
                        dir,//Random.insideUnitSphere.normalized,
                        out RaycastHit hit,
                        radius,
                        origin.LayersToDamage
                ))
                {
                    ObjectDamageManager.InflictDamage(hit.transform, new DamageData()
                    {
                        InflictorID = origin.ID,
                        ImpactType = DamageImpactType.ProjectileImpact,
                        ElementType = elementType,
                        RealDamage = Random.Range(dmgEquation.x - dmgEquation.x * dmgEquation.y, dmgEquation.x + dmgEquation.x * dmgEquation.y),
                        RealPenetration = Random.Range(penEquation.x - penEquation.x * penEquation.y, penEquation.x + penEquation.x * penEquation.y)
                    });
                }
            }
        }
    }
}
