using U3.Item;
using U3.Log;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponMaster : MonoBehaviour, IItemInputProvider
    {
        [SerializeField] WeaponSettings weaponSettings;

        public bool IsLoaded { get; set; }
        public bool IsShooting { get; set; }
        public bool IsReloading { get; set; }

        public WeaponSettings WeaponSettings => weaponSettings;

        public ItemMaster ItemMaster { get; private set; }

        public delegate void WeaponFireInputEventHandler(FireInputOrigin inputOrigin);

        public event WeaponFireInputEventHandler EventFireDownCalled;
        public event WeaponFireInputEventHandler EventFireUpCalled;

        public delegate void WeaponAimInputEventHandler();

        public event WeaponAimInputEventHandler EventAimDownCalled;
        public event WeaponAimInputEventHandler EventAimUpCalled;

        public delegate void WeaponReloadInputEventHandler(IAmmoStore ammoStore);

        public event WeaponReloadInputEventHandler EventReloadCalled;

        public delegate void WeaponEventsHandler();

        public event WeaponEventsHandler EventFireCalledOnUnloaded;

        public void CallEventFireDownCalled(FireInputOrigin inputOrigin) => EventFireDownCalled?.Invoke(inputOrigin);
        public void CallEventFireUpCalled(FireInputOrigin inputOrigin) => EventFireUpCalled?.Invoke(inputOrigin);
        public void CallEventAimDownCalled() => EventAimDownCalled?.Invoke();
        public void CallEventAimUpCalled() => EventAimUpCalled?.Invoke();
        public void CallEventReloadCalled(IAmmoStore ammoStore) => EventReloadCalled?.Invoke(ammoStore);
        public void CallEventFireCalledOnUnloaded() => EventFireCalledOnUnloaded?.Invoke();

        private void Awake()
        {
            if (TryGetComponent(out ItemMaster itemMaster))
                ItemMaster = itemMaster;
            else
                GameLogger.Log(new GameLog(Log.LogType.Error,
                    $"The weapon {gameObject.name} is missing mandatory component: ItemMaster"));
        }
    }
}