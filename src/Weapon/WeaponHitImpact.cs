
namespace U3.Weapon
{
    public class WeaponHitImpact : DamageInflictor
    {
        private WeaponMaster weaponMaster;

        private void OnEnable()
        {
            if (weaponMaster == null)
                weaponMaster = GetComponent<WeaponMaster>();

            weaponMaster.EventObjectHit += ApplyHitImpact;
        }

        private void OnDisable()
        {
            weaponMaster.EventObjectHit -= ApplyHitImpact;
        }

        private float CalcPenetration(float dmg)
        {
            if (penEquation.x == 0)
                return 1f;
            
            float pen = dmg * penEquation.x;
            return Random.Range(pen - penEquation.y, pen + penEquation.y);
        }

        private float CalcDamage(Transform hitTransform)
        {
            float dist = (m_Transform.position - hitTransform.position).magnitude;
            if (dist < 1)
                dist = 1;

            return dist * dmgEquation.x + dmgEquation.y;
        }

        private void ApplyHitImpact(FireInputOrigin inputOrigin, Transform hitTransform)
        {
            float dmg = ;
            ObjectDamageManager.InflictDamage(hitTransform, new DamageData()
                {
                    InflictorID = inputOrigin.ID,
                    ImpactType = impactType,
                    ElementType = elementType,
                    RealDamage = dmg,
                    RealPenetration = CalcPenetration(dmg)
                });
        }
    }
}