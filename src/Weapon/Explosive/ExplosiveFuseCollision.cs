using System.Collections;
using U3.Destructible;
using U3.Item;
using UnityEngine;

namespace U3.Weapon.Explosive
{
    public class ExplosiveFuseCollision : DamageInflictor
    {
        private bool isArmed;
        private ExplosiveMaster m_Explosive;

        private IEnumerator Ignite()
        {
            yield return new WaitForSeconds(m_Explosive.DmgSettings.ExplosiveSetting.FuseDelaySeconds);
            m_Explosive.Explode(new FireInputOrigin(m_DmgData.InflictorID, m_DmgData.LayersToHit, m_DmgData.LayersToDamage));
        }

        private void OnCollisionEnter(Collision col)
        {
            if (!isArmed || (m_DmgData.LayersToHit.value & (1 << col.gameObject.layer)) == 0)
                return;

            StartCoroutine(Ignite());
        }

        private IEnumerator ArmFuse()
        {
            yield return new WaitForSeconds(m_Explosive.DmgSettings.ExplosiveSetting.FuseActivationDelaySeconds);
            isArmed = true;
        }

        private void Start()
        {
            m_Explosive = GetComponent<ExplosiveMaster>();
            StartCoroutine(ArmFuse());
        }
    }
}