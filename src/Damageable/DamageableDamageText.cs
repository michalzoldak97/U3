using U3.Core;
using U3.ObjectPool;
using UnityEngine;

namespace U3.Damageable
{
    public class DamageableDamageText : Vassal<DamageableMaster>
    {
        private Transform m_Transform;

        public override void OnMasterEnabled(DamageableMaster master)
        {
            base.OnMasterEnabled(master);

            Master.EventHealthChanged += ShowDamageText;
        }

        private void OnDisable()
        {
            Master.EventHealthChanged -= ShowDamageText;
        }
        private void ShowDamageText(float change)
        {
            if (change <= 0)
                return;

            PooledObject<DamageText> dmgText = ObjectPoolsManager.Instance.GetDamageText(DamageTextManager.DamageTextPoolCode);
            dmgText.Obj.SetActive(true);
            dmgText.ObjTransform.SetPositionAndRotation(m_Transform.position, m_Transform.rotation);
            dmgText.ObjInterface.Play(change.ToString("N1"));
        }

        private void Start()
        {
            m_Transform = transform;
        }
    }
}
