using UnityEngine;

namespace U3.Inventory
{
    public interface IInventoryMaster
    {
        public void CallEventAddItem(Transform item);
    }
}
