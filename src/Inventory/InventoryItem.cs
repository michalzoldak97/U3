using U3.Item;
using UnityEngine;

namespace U3.Inventory
{
    public class InventoryItem
    {
        public bool IsSelected { get; set; }
        public ItemType Type { get; set; }
        public Collider[] Colliders { get; set; }
        public Rigidbody[] RBs { get; set; }
        public GameObject Object { get; set; }
        public ItemMaster ItemMaster { get; set; }
    }
}
