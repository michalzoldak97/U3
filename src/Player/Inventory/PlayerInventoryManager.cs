using U3.Input;
using U3.Inventory;
using U3.Item;
using UnityEngine;
using UnityEngine.InputSystem;

namespace U3.Player.Inventory
{
    public class PlayerInventoryManager : InventoryManager
    {
        private readonly int[] activeSlotIDX = new int[2];
        private PlayerInventoryMaster playerInventoryMaster;
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

            InputManager.PlayerInputActions.Humanoid.ItemThrow.performed += RemoveItem;
            InputManager.PlayerInputActions.Humanoid.ItemThrow.Enable();

            InputManager.PlayerInputActions.Humanoid.ChangeActiveInventorySlot1.performed += ChangeActiveSlot1;
            InputManager.PlayerInputActions.Humanoid.ChangeActiveInventorySlot1.Enable();

            InputManager.PlayerInputActions.Humanoid.ChangeActiveInventorySlot2.performed += ChangeActiveSlot2;
            InputManager.PlayerInputActions.Humanoid.ChangeActiveInventorySlot2.Enable();

            InputManager.PlayerInputActions.Humanoid.ChangeActiveInventorySlot3.performed += ChangeActiveSlot3;
            InputManager.PlayerInputActions.Humanoid.ChangeActiveInventorySlot3.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            playerInventoryMaster.EventChangeActiveSlot -= ChangeActiveSlot;
            playerInventoryMaster.EventAddItemToContainer -= AddItemToContainer;
            playerInventoryMaster.EventRemoveItemFromContainer -= RemoveItemFromContainer;

            InputManager.PlayerInputActions.Humanoid.ItemThrow.performed -= RemoveItem;
            InputManager.PlayerInputActions.Humanoid.ItemThrow.Disable();

            InputManager.PlayerInputActions.Humanoid.ChangeActiveInventorySlot1.performed -= ChangeActiveSlot1;
            InputManager.PlayerInputActions.Humanoid.ChangeActiveInventorySlot1.Disable();

            InputManager.PlayerInputActions.Humanoid.ChangeActiveInventorySlot2.performed -= ChangeActiveSlot2;
            InputManager.PlayerInputActions.Humanoid.ChangeActiveInventorySlot2.Disable();

            InputManager.PlayerInputActions.Humanoid.ChangeActiveInventorySlot3.performed -= ChangeActiveSlot3;
            InputManager.PlayerInputActions.Humanoid.ChangeActiveInventorySlot3.Disable();
        }

        private void SelectSlotItem()
        {
            Transform item = playerInventoryMaster.Slots[activeSlotIDX[0]].Containers[activeSlotIDX[1]].Item;

            if (item == null || // if doesn't exists
                    (currentItem != null &&
                    item == currentItem.Object.transform)) // or is already selected
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

                int numChecked = 0;

                while (numChecked < playerInventoryMaster.Slots[slotIDX].Containers.Length)
                {
                    int cIDXToSet = activeSlotIDX[1] + 1 >=
                        playerInventoryMaster.Slots[slotIDX].Containers.Length ? 0 : activeSlotIDX[1] + 1;

                    activeSlotIDX[1] = cIDXToSet;

                    if (playerInventoryMaster.Slots[slotIDX].Containers[cIDXToSet].Item != null)
                    {
                        SelectSlotItem();
                        return;
                    }

                    numChecked++;
                }
            }

            activeSlotIDX[0] = slotIDX;

            for (int i = 0; i < playerInventoryMaster.Slots[slotIDX].Containers.Length; i++) // select first occupied container
            {
                if (playerInventoryMaster.Slots[slotIDX].Containers[i].Item != null)
                {
                    activeSlotIDX[1] = i;
                    SelectSlotItem();
                    return;
                }
            }

            activeSlotIDX[1] = 0; // if all containers are empty set to 1st
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

            if (containerIDX[0] == activeSlotIDX[0] &&
                containerIDX[1] == activeSlotIDX[1])
                SelectSlotItem();
        }

        private void RemoveItemFromContainer(int[] containerIDX, Transform item)
        {
            AddItemToContainer(containerIDX, null);

            if (containerIDX[0] == activeSlotIDX[0] &&
                containerIDX[1] == activeSlotIDX[1])
                inventoryMaster.CallEventDeselectItem(currentItem.Object.transform);

            playerInventoryMaster.Items[item].IsAssignedToSlot = false;
            playerInventoryMaster.CallEventItemRemovedFromContainer();
        }

        private void RemoveItem(InputAction.CallbackContext obj)
        {
            Transform item = playerInventoryMaster.Slots[activeSlotIDX[0]].Containers[activeSlotIDX[1]].Item;

            if (item == null)
                return;

            RemoveItemFromContainer(activeSlotIDX, item);
            playerInventoryMaster.CallEventRemoveItem(item);
        }

        private void ChangeActiveSlot1(InputAction.CallbackContext obj)
        {
            playerInventoryMaster.CallEventChangeActiveSlot(0);
        }
        private void ChangeActiveSlot2(InputAction.CallbackContext obj)
        {
            playerInventoryMaster.CallEventChangeActiveSlot(1);
        }
        private void ChangeActiveSlot3(InputAction.CallbackContext obj)
        {
            playerInventoryMaster.CallEventChangeActiveSlot(2);
        }
    }
}
