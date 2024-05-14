using U3.Core;
using UnityEngine;

namespace U3.Destructible
{
    public class DamagableHitBox : Vassal<DamagableMaster>, IDamageReciever
    {
        [SerializeField] private DamagableSettings dmgSettings;

        public override void OnMasterEnabled(DamagableMaster master)
        {
            base.OnMasterEnabled(master);
        }

        public void CallEventReceiveDamage(DamageData dmgData)
        {
            throw new System.NotImplementedException();
        }
    }
}
