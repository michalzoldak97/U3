using UnityEngine;
using U3.Inventory;

namespace U3.Player.Inventory
{
    public class PlayerInventoryMaster : InventoryMaster
    {
        [SerializeField] private Transform itemContainer;

        private void Awake()
        {
            ItemContainer = itemContainer;
        }
    }
}
