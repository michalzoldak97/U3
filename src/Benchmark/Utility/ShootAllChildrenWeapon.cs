using System.Collections;
using U3.AI.Team;
using U3.Item;
using U3.Weapon;
using UnityEngine;

namespace U3.Benchmark.Utility
{
    public class ShootAllChildrenWeapon : MonoBehaviour
    {
        WeaponMaster[] weaponMasters;

        private IEnumerator ShootAllWeapons(FireInputOrigin fireInputOrigin)
        {
            yield return new WaitForSeconds(2f);
            foreach (WeaponMaster weapon in weaponMasters)
            {
                weapon.CallEventFireDownCalled(fireInputOrigin);
            }
        }

        private void Start()
        {
            weaponMasters = GetComponentsInChildren<WeaponMaster>();
            TeamIdentifier teamSetting = GetComponent<TeamIdentifier>();
            StartCoroutine(ShootAllWeapons(new FireInputOrigin(gameObject.GetInstanceID(), teamSetting.LayersToHit, teamSetting.LayersToDamage)));
        }
    }
}
