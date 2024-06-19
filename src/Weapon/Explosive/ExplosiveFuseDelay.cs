using UnityEngine;
using U3.Item;
using System.Collections;
using U3.Destructible;

namespace U3.Weapon.Explosive
{
    public class ExplosiveFuseDelay : DamageInflictor
    {
        private ExplosiveMaster m_Explosive;

        private IEnumerator Ignite()
        {
            yield return new WaitForSeconds(m_Explosive.DmgSettings.ExplosiveSetting.FuseDelaySeconds);
            m_Explosive.Explode(new FireInputOrigin(m_DmgData.InflictorID, m_DmgData.LayersToHit, m_DmgData.LayersToDamage));
        }

        private void Start ()
        {
            m_Explosive = GetComponent<ExplosiveMaster>();
            StartCoroutine(Ignite());
        }
    }
}