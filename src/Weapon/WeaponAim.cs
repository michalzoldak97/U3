using U3.Global;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponAim : Vassal<WeaponMaster>
    {
        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            Master.EventAimDownCalled += OnAimStart;
            Master.EventAimUpCalled += OnAimStop;

            Master.EventInputInterrupted += OnAimStop;
        }

        public override void OnMasterDisabled()
        {
            base.OnMasterDisabled();

            Master.EventAimDownCalled -= OnAimStart;
            Master.EventAimUpCalled -= OnAimStop;

            Master.EventInputInterrupted -= OnAimStop;
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
