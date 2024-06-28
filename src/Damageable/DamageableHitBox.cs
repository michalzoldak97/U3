using U3.Core;
using U3.Log;

namespace U3.Damageable
{
    public class DamageableHitBox : Vassal<DamageableMaster>
    {
        protected DamageableMaster damageTarget;

        protected void FetchDamageTarget()
        {
            if (damageTarget != null)
                return;

            if (transform.root.TryGetComponent(out DamageableMaster dmgTarget))
            {
                damageTarget = dmgTarget;
            }
            else
            {
                GameLogger.Log(new GameLog(LogType.Error, $"Required DamageableMaster not found on object {transform.root.gameObject.name}"));
            }
        }
    }
}