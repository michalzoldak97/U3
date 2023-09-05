using UnityEngine;

namespace U3.Item
{
    public class ItemMaster : MonoBehaviour
    {
        [SerializeField] private ItemSettings itemSettings;

        public ItemSettings ItemSettings { get { return itemSettings; } private set { } }

        public delegate void ItemInteractionEventHandler(Transform origin);
        public event ItemInteractionEventHandler EventInteractionCalled;
        public event ItemInteractionEventHandler EventPickUp;
        public event ItemInteractionEventHandler EventThrow;

        public delegate void ItemInventoryEventHandler();
        public event ItemInventoryEventHandler EventSelected;
        public event ItemInventoryEventHandler EventDeselected;
        public event ItemInventoryEventHandler EventAddedToInventory;
        public event ItemInventoryEventHandler EventRemovedFromInventory;

        public delegate void ItemInputEventHandler(int idx);
        public event ItemInputEventHandler EventInputCalled;

        public void CallEventInteractionCalled(Transform origin)
        {
            EventInteractionCalled?.Invoke(origin);
        }

        public void CallEventPickUp(Transform origin)
        {
            EventPickUp?.Invoke(origin);
        }

        public void CallEventThrow(Transform origin)
        {
            EventThrow?.Invoke(origin);
        }

        public void CallEventSelected()
        {
            EventSelected?.Invoke();
        }

        public void CallEventDeselected()
        {
            EventDeselected?.Invoke();
        }

        public void CallEventAddedToInventory()
        {
            EventAddedToInventory?.Invoke();
        }

        public void CallEventRemovedFromInventory()
        {
            EventRemovedFromInventory?.Invoke();
        }

        public void CallEventInputCalled(int idx)
        {
            EventInputCalled?.Invoke(idx);
        }

    }
}
