using U3.Inventory;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class PlayerInventoryMaster : InventoryMaster
    {
        [SerializeField] private GameObject itemCamera;
        public Slot[] Slots { get; set; }

        public delegate void PlayerInventorySlotEventHandler(int slotIDX);

        public event PlayerInventorySlotEventHandler EventChangeActiveSlot;

        public delegate void PlayerInventoryContainerEventHandler(int[] containerIDX, Transform item);

        public event PlayerInventoryContainerEventHandler EventAddItemToContainer;
        public event PlayerInventoryContainerEventHandler EventRemoveItemFromContainer;

        public delegate void PlayerInventoryContainerCallbackEventHandler();
        public event PlayerInventoryContainerCallbackEventHandler EventItemAddedToContainer;
        public event PlayerInventoryContainerCallbackEventHandler EventItemRemovedFromContainer;
        public event PlayerInventoryContainerCallbackEventHandler EventInventoryUIReloadRequest;

        public void CallEventChangeActiveSlot(int slotIDX)
        {
            EventChangeActiveSlot?.Invoke(slotIDX);
        }
        public void CallEventAddItemToContainer(int[] containerIDX, Transform item)
        {
            EventAddItemToContainer?.Invoke(containerIDX, item);
        }
        public void CallEventRemoveItemFromContainer(int[] containerIDX, Transform item)
        {
            EventRemoveItemFromContainer?.Invoke(containerIDX, item);
        }

        public void CallEventItemAddedToContainer()
        {
            EventItemAddedToContainer?.Invoke();
        }
        public void CallEventItemRemovedFromContainer()
        {
            EventItemRemovedFromContainer?.Invoke();
        }
        public void CallEventInventoryUIReloadRequest()
        {
            EventInventoryUIReloadRequest?.Invoke();
        }

        private void Start()
        {
            if (itemCamera == null) 
            {
                Log.GameLogger.Log(Log.LogType.Warning, "no item camera set on player");
                return;
            }
            
            ItemCamera = itemCamera;
        }
    }
}
