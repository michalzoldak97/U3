namespace U3.Item
{
    public interface IItemInputProvider
    {
        public void CallEventFireDownCalled(ItemInputOrigin inputOrigin);
        public void CallEventFireUpCalled(ItemInputOrigin inputOrigin);
        public void CallEventAimDownCalled(ItemInputOrigin inputOrigin);
        public void CallEventAimUpCalled(ItemInputOrigin inputOrigin);
        public void CallEventReloadCalled(ItemInputOrigin inputOrigin);
    }
}
