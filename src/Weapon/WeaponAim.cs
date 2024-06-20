using U3.Item;
using U3.Core;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponAim : Vassal<WeaponMaster>, IAimable
    {
        private bool isAiming;

        public float ItemParentAimFOV => Master.ItemMaster.ItemSettings.ItemParentAimFOV;

        public Vector3 ItemParentAimPosition => Master.ItemMaster.ItemSettings.ItemParentAimPosition;

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

        private void SetAimPosition(bool aimStateToSet, Vector3 toPos)
        {
            if (isAiming == aimStateToSet)
                return;

            transform.localPosition = toPos;

            isAiming = aimStateToSet;
        }

        private void OnAimStart() => SetAimPosition(true, Master.WeaponSettings.WeaponAimPosition);

        private void OnAimStop() => SetAimPosition(false, Master.ItemMaster.ItemSettings.OnParentPosition);
    }
}
