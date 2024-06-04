using System.Collections.Generic;
using U3.Core;
using U3.Destructible;
using U3.Item;
using UnityEngine;

namespace U3.Weapon.Explosive
{
    public class ExplosiveMaster : MonoBehaviour, IExplosive
    {
        [SerializeField] private DamageInflictorSettings dmgSettings;

        public DamageInflictorSettings DmgSettings => dmgSettings;

        public List<Collider> ExplosionTargets { get; private set; }

        public delegate void ExplosiveEventHandler(FireInputOrigin origin);

        public event ExplosiveEventHandler EventFetchTargets;
        public event ExplosiveEventHandler EventIgniteExplosion;
        public event ExplosiveEventHandler EventExploded;

        public void CallEventFetchTargets(FireInputOrigin origin) => EventFetchTargets?.Invoke(origin);
        public void CallEventIgniteExplosion(FireInputOrigin origin) => EventIgniteExplosion?.Invoke(origin);
        public void CallEventExploded(FireInputOrigin origin) => EventExploded?.Invoke(origin);

        public void Explode(FireInputOrigin origin)
        {
            CallEventFetchTargets(origin);

            if (ExplosionTargets.Count < 1)
                return;

            CallEventIgniteExplosion(origin);
            ExplosionTargets.Clear();
        }

        private void OnEnable()
        {
            foreach (Vassal<ExplosiveMaster> vassal in GetComponents<Vassal<ExplosiveMaster>>())
            {
                vassal.OnMasterEnabled(this);
            }
        }

        private void OnDisable()
        {
            foreach (Vassal<ExplosiveMaster> vassal in GetComponents<Vassal<ExplosiveMaster>>())
            {
                vassal.OnMasterDisabled();
            }
        }

        private void Start()
        {
            ExplosionTargets = new List<Collider>(dmgSettings.ExplosiveSetting.TargetCapacity);
        }
    }
}