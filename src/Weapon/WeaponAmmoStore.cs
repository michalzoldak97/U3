using System.Collections.Generic;
using U3.Log;
using UnityEngine;

namespace U3.Weapon
{
    [System.Serializable]
    public struct WeaponAmmoData
    {
        public int Amount;
        public int Limit;
        public string Code;
        public string Name;

        public WeaponAmmoData(int amount, int limit, string code, string name)
        {
            Amount = amount; Limit = limit; Code = code; Name = name;
        }
    }

    public class WeaponAmmo
    {
        public int Amount;
        public int Limit;
        public string Code;
        public string Name;

        public WeaponAmmo(int amount, int limit, string code, string name)
        {
            Amount = amount; Limit = limit; Code = code; Name = name;
        }
    }
    public class WeaponAmmoStore : MonoBehaviour, IAmmoStore
    {
        private Dictionary<string, WeaponAmmo> ammoStore;

        protected virtual Dictionary<string, WeaponAmmo> CreateAmmoStore()
        {
            return new();
        }

        protected virtual void OnEnable()
        {
            ammoStore = CreateAmmoStore();
        }

        private bool AmmoCodeExits(string ammoCode)
        {
            if (!ammoStore.ContainsKey(ammoCode))
            {
                GameLogger.Log(new GameLog(Log.LogType.Warning,
                    $"trying to access non-existing ammo {ammoCode}"));
                return false;
            }

            return true;
        }

        public WeaponAmmoData GetAmmo(string ammoCode)
        {
            if (!AmmoCodeExits(ammoCode))
                return new();

            WeaponAmmo ammo = ammoStore[ammoCode];
            return new WeaponAmmoData(ammo.Amount, ammo.Limit, ammo.Code, ammo.Name);
        }

        public int AddAmmo(int amount, string ammoCode)
        {
            if (!AmmoCodeExits(ammoCode))
                return 0;

            int capacity = ammoStore[ammoCode].Limit - ammoStore[ammoCode].Amount;
            int toAdd = amount < capacity ? amount : capacity;

            ammoStore[ammoCode].Amount += toAdd;

            return toAdd;
        }

        public int RetrieveAmmo(int amount, string ammoCode)
        {
            if (!AmmoCodeExits(ammoCode))
                return 0;

            int toRetrieve = ammoStore[ammoCode].Amount > amount ? amount : ammoStore[ammoCode].Amount;
            ammoStore[ammoCode].Amount -= toRetrieve;

            return toRetrieve;
        }
    }
}
