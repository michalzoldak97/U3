using U3.Damageable;

namespace U3.ObjectPool
{
    public class DamageTextReturner : PooledObjectReturner
    {

        private PooledObject<DamageText> m_PooledObject;

        public override void SetPooledOject<T>(PooledObject<T> obj)
        {
            if (obj is PooledObject<DamageText> someClassObj)
            {
                m_PooledObject = someClassObj;
            }
        }

        protected override bool TryReturnToPool()
        {
            return ObjectPoolsManager.Instance.AddDamageText(PoolCode, m_PooledObject);
        }
    }
}