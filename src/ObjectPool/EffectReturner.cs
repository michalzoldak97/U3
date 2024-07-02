using U3.Weapon.Effect;

namespace U3.ObjectPool
{
    public class EffectReturner : PooledObjectReturner
    {
        private PooledObject<HitEffect> m_PooledObject;

        public override void SetPooledOject<T>(PooledObject<T> obj)
        {
            if (obj is PooledObject<HitEffect> someClassObj)
            {
                m_PooledObject = someClassObj;
            }
        }

        protected override bool TryReturnToPool()
        {
            return ObjectPoolsManager.Instance.AddEffect(PoolCode, m_PooledObject);
        }
    }
}