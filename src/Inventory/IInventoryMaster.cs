using UnityEngine;

namespace U3.Inventory
{
    public interface IInventoryMaster
    {
        // events
        public void CallEventAddItem(Transform item);
        public void CallEventItemAdded(Transform item);
        public void CallEventRemoveItem(Transform item);
        public void CallEventItemRemoved(Transform item);
    }
}
