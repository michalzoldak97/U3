using UnityEngine;
using U3.Inventory;
using System.Collections.Generic;
using U3.Item;
using System.Linq;

namespace U3.Player.Inventory
{
    public class PlayerInventoryMaster : InventoryMaster, IInventoryUIEventsMaster
    {
        [SerializeField] private Transform itemContainer;

        public Transform FocusedItem { get; set; }
        public PlayerMaster PlayerMaster { get; private set; }
        public List<IItemSlot> ItemSlots { get; private set; }
        public Dictionary<int, IItemSlot> SelectableItemSlots { get; private set; }

        public event InventoryItemEventHandler EventSlotSelected;

        public void CallEventSlotSelected(Transform item) => EventSlotSelected?.Invoke(item);

        public delegate void InventoryUIEventHandler();
        public event InventoryUIEventHandler EventReloadBackpack;

        public void CallEventReloadBackpack() => EventReloadBackpack?.Invoke();

        public delegate void InventoryUIDragDropEventHandler(IItemButton itemButton, IInventoryDropArea dropArea);
        public event InventoryUIDragDropEventHandler EventOnItemButtonDrop;

        public void CallEventOnItemButtonDrop(IItemButton itemButton, IInventoryDropArea dropArea) => EventOnItemButtonDrop?.Invoke(itemButton, dropArea);

        public delegate void InventoryUIItemFocusEventHandler(Transform focusedItem);
        public event InventoryUIItemFocusEventHandler EventItemFocused;
        public event InventoryUIItemFocusEventHandler EventItemUnfocused;

        public void CallEventItemFocused(Transform item) => EventItemFocused?.Invoke(item);
        public void CallEventItemUnfocused(Transform item) => EventItemUnfocused?.Invoke(item);

        public delegate void InventorySlotsEventHandler(int slotIndex);
        public event InventorySlotsEventHandler EventSelectSlot;

        public void CallEventSelectSlot(int slotIndex) => EventSelectSlot?.Invoke(slotIndex);

        public IEnumerable<InventoryItem> GetBackpackItems()
        {
            InventoryItem[] allItems = Items.GetAllItems();
            HashSet<InventoryItem> slotItems = ItemSlots.Select(item => item.AssignedItem).ToHashSet();

            return allItems.Where(item => !slotItems.Contains(item));
        }

        public override bool IsInventoryAvailableForItem(ItemType itemType)
        {
            int eligibleSlotsNum = ItemSlots.Where(slot => slot.AcceptableItemTypes.Contains(itemType) && slot.AssignedItem == null).Count();

            if (eligibleSlotsNum > 0)
                return true;

            return GetBackpackItems().Count() < PlayerMaster.PlayerSettings.Inventory.BackpackCapacity;
        }

        public void AddSelectableSlot(int index, IItemSlot slotToAdd)
        {
            SelectableItemSlots.Add(index, slotToAdd);
        }

        private void Awake()
        {
            ItemContainer = itemContainer;
            PlayerMaster = GetComponent<PlayerMaster>();
            ItemSlots = new(PlayerMaster.PlayerSettings.Inventory.InventorySlots.Length);
            SelectableItemSlots = new();
        }
    }
}
