using System.Collections;
using U3.AI.Team;
using U3.Destructible;
using U3.Item;
using UnityEngine;

namespace U3.Weapon.Explosive
{
    public class ExplosiveFuseMine : DamageInflictor
    {
        private ExplosiveMaster m_Explosive;

        private IEnumerator Ignite()
        {
            yield return new WaitForSeconds(m_Explosive.DmgSettings.ExplosiveSetting.FuseDelaySeconds);
            m_Explosive.Explode(new FireInputOrigin(m_DmgData.InflictorID, m_DmgData.LayersToHit, m_DmgData.LayersToDamage));
        }

        private void OnCollisionEnter(Collision col)
        {
            if ((m_DmgData.LayersToHit.value & (1 << col.gameObject.layer)) != 0 &&
                col.transform.root.TryGetComponent(out Rigidbody rb) &&
                rb.mass > dmgSettings.MineSetting.FusePreassureKG)
                    StartCoroutine(Ignite());
        }

        private void SetDamageData()
        {
            m_DmgData.InflictorID = TeamManager.GetTeamInstanceID(dmgSettings.MineSetting.InflictorTeam);
            m_DmgData.LayersToHit = dmgSettings.MineSetting.LayersToIgniteOn;
            m_DmgData.LayersToDamage = dmgSettings.MineSetting.LayersToDamage;
        }

        private void Start()
        {
            m_Explosive = GetComponent<ExplosiveMaster>();
            SetDamageData();
        }
    }
}