using U3.Core;
using U3.Input;

namespace U3.Weapon
{
    public class WeaponFireMode : Vassal<WeaponMaster>
    {
        public override void OnMasterEnabled(WeaponMaster weaponMaster)
        {
            base.OnMasterEnabled(weaponMaster);

            PlayerInputManager.HumanoidInputActions.EventChangeWeaponMode += ChangeFireMode;
        }

        public override void OnMasterDisabled()
        {
            base.OnMasterDisabled();

            PlayerInputManager.HumanoidInputActions.EventChangeWeaponMode -= ChangeFireMode;
        }

        private void ChangeFireMode()
        {
            FireMode[] fireModes = Master.WeaponSettings.GunSettings.AvailableFireModes;

            if (fireModes.Length < 2)
                return;

            int nextMode = (int)Master.FireMode + 1 < fireModes.Length ? (int)Master.FireMode + 1 : 0;

            Master.FireMode = (FireMode)nextMode;
            Master.CallEventFireModeChanged((FireMode)nextMode);
        }

        private void Start()
        {
            Master.FireMode = Master.WeaponSettings.GunSettings.DeafaultFireMode;
        }
    }
}
