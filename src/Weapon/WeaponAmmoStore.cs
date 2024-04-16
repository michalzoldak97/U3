using UnityEngine;

namespace U3.Weapon
{
    public class WeaponAmmoStore : MonoBehaviour, IAmmoStore
    {
        public int AddAmmo(int amount, string ammoCode)
        {
            throw new System.NotImplementedException();
        }

        public int GetAmmo(string ammoCode)
        {
            return 0;
        }

        public int RetrieveAmmo(int amount, string ammoCode)
        {
            throw new System.NotImplementedException();
        }
    }
}
