using U3.Item;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponFire : MonoBehaviour
    {
        // on fire
        // if shooting do nothing
        // if reloading do nothing
        // if not loaded do click and wait
        // on disable reset states -> weapon mamager
        private WeaponMaster weaponMaster;

        private void SetInit()
        {
            if (weaponMaster == null)
                weaponMaster = GetComponent<WeaponMaster>();
        }

        private void OnEnable()
        {
            SetInit();

            weaponMaster.EventFireDownCalled += OnFireStart;
            weaponMaster.EventFireUpCalled += OnFireStop;
        }

        private void OnDisable()
        {
            weaponMaster.EventFireDownCalled -= OnFireStart;
            weaponMaster.EventFireUpCalled -= OnFireStop;
        }

        private void OnFireStart(FireInputOrigin inputOrigin)
        {
            Debug.Log($"Fire called by {inputOrigin.Name} with id {inputOrigin.ID} on weapon {gameObject.name} with id {transform.GetInstanceID()}");
        }

        private void OnFireStop(FireInputOrigin inputOrigin)
        {
            Debug.Log($"Fire STOP called by {inputOrigin.Name} with id {inputOrigin.ID} on weapon {gameObject.name} with id {transform.GetInstanceID()}");
        }
    }
}
