using UnityEngine;
using U3.Inventory;
using System.Collections.Generic;

namespace U3.Player.Inventory
{
    public class PlayerInventoryMaster : InventoryMaster, IInventoryUIEventsMaster
    {
        // TODO: check if inventory is full, it should depend on backapck and item type to pick up vs free slots acceptable types
        [SerializeField] private Transform itemContainer;

        public PlayerMaster PlayerMaster { get; private set; }
        public List<IItemSlot> ItemSlots { get; private set; }

        public delegate void InventoryUIEventHandler(IItemButton itemButton, IInventoryDropArea dropArea);
        public event InventoryUIEventHandler EventOnItemButtonDrop;

        public void CallEventOnItemButtonDrop(IItemButton itemButton, IInventoryDropArea dropArea)
        {
            EventOnItemButtonDrop?.Invoke(itemButton, dropArea);
        }

        private void Awake()
        {
            ItemContainer = itemContainer;
            PlayerMaster = GetComponent<PlayerMaster>();
            ItemSlots = new(PlayerMaster.PlayerSettings.Inventory.InventorySlots.Length);
        }
    }
}
