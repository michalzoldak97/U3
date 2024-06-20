using U3.Core;
using U3.Destructible;
using UnityEngine;

namespace U3.Damageable
{
    public class DamageableHitBox : Vassal<DamageableMaster>, IDamageReciever
    {
        [SerializeField] private DamageableSettings dmgSettings;

        public override void OnMasterEnabled(DamageableMaster master)
        {
            base.OnMasterEnabled(master);
        }

        public void CallEventReceiveDamage(DamageData dmgData)
        {
            throw new System.NotImplementedException();
        }
    }
}
