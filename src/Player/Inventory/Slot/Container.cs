using UnityEngine;

namespace U3.Player.Inventory
{
    public class Container
    {
        public int IDX { get; private set; }
        public Transform Item { get; set; }

        public Container(int idx)
        {
            IDX = idx;
        }
    }
}
