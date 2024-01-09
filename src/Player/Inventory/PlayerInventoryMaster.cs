using UnityEngine;
using U3.Inventory;

namespace U3.Player.Inventory
{
    public class PlayerInventoryMaster : InventoryMaster
    {
        [SerializeField] private Transform itemContainer;

        public PlayerMaster PlayerMaster { get; private set; }

        private void Awake()
        {
            ItemContainer = itemContainer;
            PlayerMaster = GetComponent<PlayerMaster>();
        }
    }
}
