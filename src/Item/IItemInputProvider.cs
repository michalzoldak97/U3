using U3.Weapon;

namespace U3.Item
{
    public interface IItemInputProvider
    {
        public void CallEventFireDownCalled(FireInputOrigin inputOrigin);
        public void CallEventFireUpCalled(FireInputOrigin inputOrigin);
        public void CallEventAimDownCalled();
        public void CallEventAimUpCalled();
        public void CallEventReloadCalled(IAmmoStore ammoStore);
        public void CallEventInputInterrupted();
    }
}
