using UnityEngine;

namespace U3.Item
{
    public class ItemMaster : MonoBehaviour
    {
        [SerializeField] private ItemSettings itemSettings;

        public bool IsSelectedOnInventory { get; private set; }
        public ItemSettings ItemSettings => itemSettings;

        public delegate void ItemInteractionEventHandler(Transform origin);
        public event ItemInteractionEventHandler EventInteractionCalled;
        public event ItemInteractionEventHandler EventPickUp;
        public event ItemInteractionEventHandler EventThrow;

        public delegate void ItemInventoryEventHandler();
        public event ItemInventoryEventHandler EventSelected;
        public event ItemInventoryEventHandler EventDeselected;
        public event ItemInventoryEventHandler EventAddedToInventory;
        public event ItemInventoryEventHandler EventRemovedFromInventory;
        public event ItemInventoryEventHandler EventActionActivated;
        public event ItemInventoryEventHandler EventActionDeactivated;

        public void CallEventInteractionCalled(Transform origin) => EventInteractionCalled?.Invoke(origin);

        public void CallEventPickUp(Transform origin) => EventPickUp?.Invoke(origin);

        public void CallEventThrow(Transform origin) => EventThrow?.Invoke(origin);

        public void CallEventSelected()
        {
            EventSelected?.Invoke();
            IsSelectedOnInventory = true;
        }

        public void CallEventDeselected()
        {
            EventDeselected?.Invoke();
            IsSelectedOnInventory = false;
        }

        public void CallEventAddedToInventory() => EventAddedToInventory?.Invoke();

        public void CallEventRemovedFromInventory() => EventRemovedFromInventory?.Invoke();

        public void CallEventActionActivated() => EventActionActivated?.Invoke();

        public void CallEventActionDeactivated() => EventActionDeactivated?.Invoke();
    }
}
