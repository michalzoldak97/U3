using U3.Item;

namespace U3.Weapon
{
    public class GunShootRaycast : GunShoot
    {
        private DamageImpactType impactType;
        private DamageElementType elementType;
        private LayerMask layersToHit;
        private LayerMask layersToDamage;
        private float range;
        private Vector2 penEquation;
        private Vector2 dmgEquation;

        private void CalcPenetration(float dmg)
        {
            if (penEquation.x == 0)
                return 1f;
            
            float pen = dmg * penEquation.x;
            return Random.Range(pen - penEquation.y, pen + penEquation.y);
        }

        private float CalcDamage(RaycastHit hit)
        {
            float dist = (myTransform.position - hit.transform.position).magnitude;
            if (dist < 1)
                dist = 1;

            return dist * funcCoeff + funcInter
        }

        private void ApplyDamage(FireInputOrigin inputOrigin, RaycastHit hit)
        {
            float dmg = CalcDamage(hit);
            ObjectDamageManager.InflictDamage(hit.transform, new DamageData()
                {
                    InflictorID = inputOrigin.ID,
                    InflictorTeamID = inputOrigin.TeamID,
                    ImpactType = impactType,
                    ElementType = elementType,
                    RealDamage = dmg,
                    RealPenetration = CalcPenetration(dmg)
                });
        }

        protected void ShootRaycast(FireInputOrigin inputOrigin)
        {
            if (Physics.Raycast(
                m_Transform.TransformPoint(startPos),
                m_Transform.TransformDirection(
                    Random.Range(-recoil.x, recoil.x),
                    Random.Range(-recoil.y, recoil.y),
                    startPos.z
                ),
                out RaycastHit hit,
                range,
                layersToHit
            ))
            {
                int hitLayer = hit.transform.gameObject.layer;

                if ((layersToDamage & (1 << hitLayer)) != 0)
                {
                    ApplyDamage(hit);
                    SpawnHitEffect(col);
                }
                else if ((layersToHit & (1 << hitLayer)) != 0)
                    SpawnHitEffect(col);
                }
        }

        protected override void ShootAction(FireInputOrigin inputOrigin)
        {
            ShootRaycast(inputOrigin);
        }

        protected override void Start()
        {
            layersToHit = Master.WeaponSettings.LayersToHit;
            layersToDamage = Master.WeaponSettings.LayersToDamage;
            range = Master.WeaponSettings.FireRange;
            penCoeff = Master.WeaponSettings.RaycastDamageSettings.PenetrationCoeff;
            dmgEquation = Master.WeaponSettings.RaycastDamageSettings.DamageEquation;
            impactType = Master.WeaponSettings.RaycastDamageSettings.ImpactType;
            elementType = Master.WeaponSettings.RaycastDamageSettings.ElementType;
        }
    }
}
