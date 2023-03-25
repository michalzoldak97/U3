using U3.Inventory;
using U3.Item;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class PlayerInventoryManager : InventoryManager
    {
        private int[] activeSlotIDX = new int[2];
        private PlayerInventoryMaster playerInventoryMaster;
        // TODO: handle when picking up item when none is selected, it should be moved to 1st free slot or deactivated
        // hande adding and removing from slot
        private void LoadItems()
        {
            foreach (Transform iT in itemContainer)
            {
                if (iT.TryGetComponent<ItemMaster>(out _))
                {
                    inventoryMaster.CallEventAddItem(iT);
                }
            }
        }
        private void LoadInventorySlots()
        {
            InventorySlotSetting[] slots = GetComponent<PlayerMaster>().PlayerSettings.Inventory.InventorySlots;

            playerInventoryMaster.Slots = new Slot[slots.Length];

            for (int i = 0; i < slots.Length; i++)
            {
                playerInventoryMaster.Slots[i] = new Slot(slots[i].SlotName, slots[i].ContainerNum);
            }
        }
        protected override void SetInit()
        {
            inventoryMaster = GetComponent<PlayerInventoryMaster>();
            playerInventoryMaster = (PlayerInventoryMaster)inventoryMaster;

            itemContainer = GetComponent<PlayerMaster>().FPSCamera;

            LoadInventorySlots();
            LoadItems();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            playerInventoryMaster.EventChangeActiveSlot += ChangeActiveSlot;
            playerInventoryMaster.EventAddItemToContainer += AddItemToContainer;
            playerInventoryMaster.EventRemoveItemFromContainer += RemoveItemFromContainer;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            playerInventoryMaster.EventChangeActiveSlot -= ChangeActiveSlot;
            playerInventoryMaster.EventAddItemToContainer -= AddItemToContainer;
            playerInventoryMaster.EventRemoveItemFromContainer -= RemoveItemFromContainer;
        }

        private void SelectSlotItem()
        {
            Transform item = playerInventoryMaster.Slots[activeSlotIDX[0]].Containers[activeSlotIDX[1]].Item;

            if (item == null ||
                item == currentItem.Object.transform)
                return;

            if (currentItem != null)
                inventoryMaster.CallEventDeselectItem(currentItem.Object.transform);

            inventoryMaster.CallEventSelectItem(item);
        }
        private void ChangeActiveSlot(int slotIDX)
        {
            if (slotIDX >= playerInventoryMaster.Slots.Length)
            {
                Log.GameLogger.Log(Log.LogType.Warning, "slot index out of range");
                return;
            }

            if (slotIDX == activeSlotIDX[0]) // if selected slot is current one incerment container idx
            {
                int cIDXToSet = activeSlotIDX[1] + 1 > 
                    playerInventoryMaster.Slots[slotIDX].Containers.Length - 1 ? 0 : activeSlotIDX[1] + 1; // if current container is last go to the first
                activeSlotIDX[1] = cIDXToSet;
                return;
            }

            activeSlotIDX[0] = slotIDX;
            activeSlotIDX[1] = 0;

            SelectSlotItem();
        }

        private void AddItemToContainer(int[] containerIDX, Transform item)
        {
            if (containerIDX[0] >= playerInventoryMaster.Slots.Length ||
                containerIDX[1] >= playerInventoryMaster.Slots[containerIDX[0]].Containers.Length)
            {
                Log.GameLogger.Log(Log.LogType.Warning, "slot or container index out of range");
                return;
            }

            playerInventoryMaster.Slots[containerIDX[0]].Containers[containerIDX[1]].Item = item;

            if (item == null)
                return;

            playerInventoryMaster.Items[item].IsAssignedToSlot = true;
            playerInventoryMaster.CallEventItemAddedToContainer();

        }

        private void RemoveItemFromContainer(int[] containerIDX, Transform item)
        {
            AddItemToContainer(containerIDX, null);

            playerInventoryMaster.Items[item].IsAssignedToSlot = false;
            playerInventoryMaster.CallEventItemRemovedFromContainer();
        }
    }
}
