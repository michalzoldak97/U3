using U3.Item;
using UnityEngine;

namespace U3.Inventory
{
    public class InventoryItem
    {
        public Transform Item { get; set; }
        public Collider[] ItemColliders { get; set; }
        public Rigidbody[] ItemRigidbodies { get; set; }
        public GameObject ItemObject { get; set; }
        public ItemMaster ItemMaster { get; set; }
    }
}
