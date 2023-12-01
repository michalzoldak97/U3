using UnityEngine;

namespace U3.Player.Inventory
{
    public interface ISlot
    {
        public bool IsSelected { get; set; }
        public int[] IDX { get; set; }
        public Transform Item { get; set; }
        public Item.ItemType[] AcceptableItemTypes { get; }
    }
}
