using U3.Core;
using UnityEngine;

namespace U3.Destructible
{
    public class DamagableMaster : MonoBehaviour, IDamageReciever
    {
        [SerializeField] private DamagableSettings damagableSettings;

        public float Health { get; set; }
        public DamagableSettings DamagableSettings => damagableSettings;

        public delegate void DamageEventsHandler(DamageData dmgData);

        public event DamageEventsHandler EventReceiveDamage;
        public event DamageEventsHandler EventObjectDestruction;
        public event DamageEventsHandler EventChangeHealth;

        public void CallEventReceiveDamage(DamageData dmgData) => EventReceiveDamage?.Invoke(dmgData);
        public void CallEventObjectDestruction(DamageData dmgData) => EventObjectDestruction?.Invoke(dmgData);
        public void CallEventChangeHealth(DamageData dmgData) => EventChangeHealth?.Invoke(dmgData);

        private void Awake()
        {
            ObjectDamageManager.RegisterDamagable(transform, this);
        }

        private void OnEnable()
        {
            foreach (Vassal<DamagableMaster> vassal in GetComponents<Vassal<DamagableMaster>>())
            {
                vassal.OnMasterEnabled(this);
            }
        }

        private void OnDisable()
        {
            foreach (Vassal<DamagableMaster> vassal in GetComponents<Vassal<DamagableMaster>>())
            {
                vassal.OnMasterDisabled();
            }
        }
    }
}
