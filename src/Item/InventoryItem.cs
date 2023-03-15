using UnityEngine;

namespace U3.Item
{
    public class InventoryItem
    {
        public bool IsSelected { get; set; }
        public ItemType Type { get; set; }
        public Collider Collider { get; set; }
        public Rigidbody RB { get; set; }
        public GameObject Object { get; set; }
        public ItemMaster ItemMaster { get; set; }
    }
}
