using UnityEngine;

namespace U3.Inventory
{
    public interface IInventoryMaster
    {
        public IInventoryStore Items { get; }
        public void CallEventAddItem(Transform item);
    }
}
