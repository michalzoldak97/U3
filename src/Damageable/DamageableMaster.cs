using U3.Core;
using UnityEngine;

namespace U3.Damageable
{
    public class DamageableMaster : MonoBehaviour, IDamageReciever
    {
        [SerializeField] private DamageableSettings damagableSettings;

        public float Health { get; set; }
        public DamageableSettings DamagableSettings => damagableSettings;

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
            foreach (Vassal<DamageableMaster> vassal in GetComponents<Vassal<DamageableMaster>>())
            {
                vassal.OnMasterEnabled(this);
            }
        }

        private void OnDisable()
        {
            foreach (Vassal<DamageableMaster> vassal in GetComponents<Vassal<DamageableMaster>>())
            {
                vassal.OnMasterDisabled();
            }
        }
    }
}
