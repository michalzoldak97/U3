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
        public event DamageEventsHandler EventReceiveProjectileDamage;
        public event DamageEventsHandler EventReceiveExplosionDamage;

        public delegate void HitBoxEventHandler(DamageData dmgData, string eventCode);

        public event HitBoxEventHandler EventHitBoxDamaged;
        public event HitBoxEventHandler EventHitBoxDestroyed;

        public void CallEventReceiveDamage(DamageData dmgData) => EventReceiveDamage?.Invoke(dmgData);
        public void CallEventObjectDestruction(DamageData dmgData) => EventObjectDestruction?.Invoke(dmgData);
        public void CallEventChangeHealth(DamageData dmgData) => EventChangeHealth?.Invoke(dmgData);
        public void CallEventReceiveProjectileDamage(DamageData dmgData) => EventReceiveProjectileDamage?.Invoke(dmgData);
        public void CallEventReceiveExplosionDamage(DamageData dmgData) => EventReceiveExplosionDamage?.Invoke(dmgData);

        public void CallEventHitBoxDamaged(DamageData dmgData, string eventCode) => EventHitBoxDamaged?.Invoke(dmgData, eventCode);
        public void CallEventHitBoxDestroyed(DamageData dmgData, string eventCode) => EventHitBoxDestroyed?.Invoke(dmgData, eventCode);

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
