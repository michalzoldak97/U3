using UnityEngine;
using U3.Inventory;

namespace U3.Player.Inventory
{
    public class PlayerInventoryMaster : InventoryMaster
    {
        // TODO: check if inventory is full, it should depend on backapck and item type to pick up vs free slots acceptable types
        [SerializeField] private Transform itemContainer;

        public PlayerMaster PlayerMaster { get; private set; }

        private void Awake()
        {
            ItemContainer = itemContainer;
            PlayerMaster = GetComponent<PlayerMaster>();
        }
    }
}
