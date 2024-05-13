using UnityEngine;

namespace U3.Destructible
{
    public class DamagableMaster : MonoBehaviour, IDamageReciever
    {
        [SerializeField] private DamagableSettings damagableSettings;
        public DamagableSettings DamagableSettings => damagableSettings;

        public delegate void DamageEventsHandler(DamageData dmgData);

        public event DamageEventsHandler EventReceiveDamage;
        public event DamageEventsHandler EventObjectDestruction;

        public void CallEventReceiveDamage(DamageData dmgData) => EventReceiveDamage?.Invoke(dmgData);

        public void CallEventObjectDestruction(DamageData dmgData) => EventObjectDestruction?.Invoke(dmgData);

        private void Awake()
        {
            ObjectDamageManager.RegisterDamagable(transform, this);
        }
    }
}
