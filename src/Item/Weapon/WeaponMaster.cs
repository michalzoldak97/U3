using U3.Log;
using UnityEngine;

namespace U3.Item.Weapon
{
    public class WeaponMaster : MonoBehaviour, IItemInputProvider
    {
        public ItemMaster ItemMaster { get; private set; }

        public delegate void WeaponInputEventHandler(ItemInputOrigin inputOrigin);

        public event WeaponInputEventHandler EventFireDownCalled;
        public event WeaponInputEventHandler EventFireUpCalled;
        public event WeaponInputEventHandler EventAimDownCalled;
        public event WeaponInputEventHandler EventAimUpCalled;
        public event WeaponInputEventHandler EventReloadCalled;

        public void CallEventFireDownCalled(ItemInputOrigin inputOrigin) => EventFireDownCalled?.Invoke(inputOrigin);
        public void CallEventFireUpCalled(ItemInputOrigin inputOrigin) => EventFireUpCalled?.Invoke(inputOrigin);
        public void CallEventAimDownCalled(ItemInputOrigin inputOrigin) => EventAimDownCalled?.Invoke(inputOrigin);
        public void CallEventAimUpCalled(ItemInputOrigin inputOrigin) => EventAimUpCalled?.Invoke(inputOrigin);
        public void CallEventReloadCalled(ItemInputOrigin inputOrigin) => EventReloadCalled?.Invoke(inputOrigin);

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
