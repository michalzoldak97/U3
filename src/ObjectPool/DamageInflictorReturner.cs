using U3.Destructible;

namespace U3.ObjectPool
{
    public class DamageInflictorReturner : PooledObjectReturner
    {
        private PooledObject<DamageInflictor> m_PooledObject;

        public override void SetPooledOject<T>(PooledObject<T> obj)
        {
            if (obj is PooledObject<DamageInflictor> someClassObj)
            {
                m_PooledObject = someClassObj;
            }
        }

        protected override bool TryReturnToPool()
        {
            return ObjectPoolsManager<DamageInflictor>.Instance.AddObject(PoolCode, m_PooledObject);
        }
    }
}
