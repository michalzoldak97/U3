using U3.Item;
using U3.Core;

namespace U3.Weapon
{
    public class WeaponAim : Vassal<WeaponMaster>, IAimable
    {
        private bool isAiming;

        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            Master.EventAimDownCalled += OnAimStart;
            Master.EventAimUpCalled += OnAimStop;

            Master.EventInputInterrupted += OnAimStop;
            Master.ItemMaster.EventDeselected += OnAimStop;
        }

        private void OnDisable()
        {
            base.OnMasterDisabled();

            Master.EventAimDownCalled -= OnAimStart;
            Master.EventAimUpCalled -= OnAimStop;

            Master.EventInputInterrupted -= OnAimStop;
            Master.ItemMaster.EventDeselected -= OnAimStop;

            OnAimStop();
        }

        private void OnAimStart()
        {
            if (isAiming)
                return;

            transform.localPosition = Master.WeaponSettings.WeaponAimPosition;

            isAiming = true;
        }

        private void OnAimStop()
        {
            if (!isAiming)
                return;

            transform.localPosition = Master.ItemMaster.ItemSettings.OnParentPosition;

            isAiming = false;
        }
    }
}
