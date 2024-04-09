using UnityEngine;

namespace U3.Weapon
{
    public class WeaponAim : MonoBehaviour
    {
        private WeaponMaster weaponMaster;

        private void SetInit()
        {
            if (weaponMaster == null)
                weaponMaster = GetComponent<WeaponMaster>();
        }

        private void OnEnable()
        {
            SetInit();

            weaponMaster.EventAimDownCalled += OnAimStart;
            weaponMaster.EventAimUpCalled += OnAimStop;
        }

        private void OnDisable()
        {
            weaponMaster.EventAimDownCalled -= OnAimStart;
            weaponMaster.EventAimUpCalled -= OnAimStop;
        }

        private void OnAimStart()
        {
            Debug.Log($"Aim called  on weapon {gameObject.name} with id {transform.GetInstanceID()}");
        }

        private void OnAimStop()
        {
            Debug.Log($"Aim STOP called on weapon {gameObject.name} with id {transform.GetInstanceID()}");
        }
    }
}
