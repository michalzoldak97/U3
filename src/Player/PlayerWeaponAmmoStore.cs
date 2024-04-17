using System.Collections.Generic;
using U3.Log;
using U3.Weapon;

namespace U3.Player
{
    public class PlayerWeaponAmmoStore : WeaponAmmoStore
    {
        protected override Dictionary<string, WeaponAmmo> CreateAmmoStore()
        {
            Dictionary<string, WeaponAmmo> playerAmmmoStore = new();

            if (TryGetComponent(out PlayerMaster playerMaster))
            {
                foreach (WeaponAmmoData weaponAmmo in playerMaster.PlayerSettings.Ammo.PlayerWeaponAmmo)
                {
                    playerAmmmoStore[weaponAmmo.Code] = new WeaponAmmo(weaponAmmo.Amount, weaponAmmo.Limit, weaponAmmo.Code, weaponAmmo.Name);
                }
            }
            else
                GameLogger.Log(new GameLog(Log.LogType.Error,
                    $"failed to initialize PlayerWeaponAmmoStore as the {transform.name} is missing required PlayerMaster component"));

            return playerAmmmoStore;
        }
    }
}
