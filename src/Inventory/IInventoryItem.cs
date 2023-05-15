using U3.Item;
using UnityEngine;

namespace U3.Inventory
{
    public interface IInventoryItem
    {
        public Collider[] ItemColliders { get; set; }
        public Rigidbody[] ItemRigidbodies { get; set; }
        public ItemMaster ItemMaster { get; set; }
    }
}
