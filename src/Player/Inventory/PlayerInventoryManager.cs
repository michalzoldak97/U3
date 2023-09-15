using U3.Inventory;
using U3.Player.Inventory;
using UnityEngine;

namespace U3
{
    public class PlayerInventoryManager : InventoryManager
    {
        private PlayerInventoryMaster playerInventoryMaster;
        protected override void SetInit()
        {
            base.SetInit();
            playerInventoryMaster = GetComponent<PlayerInventoryMaster>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            playerInventoryMaster.EventItemAdded += OnItemAdded;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            playerInventoryMaster.EventItemAdded -= OnItemAdded;
        }

        private void OnItemSelected()
        {
            // when item selected assign it to the active slot
        }
        private void UnassignItem()
        {
            // clear assigned item from slot object
        }
        private void AssignItemToSlot(Transform item, int[] idx)
        {
            // if -1 -1 unassign item
            // if slot occupied release item
                // call assign item to slot with -1 -1 
            // assign slot object with item
            // check if was assigned to any other slot
                // clear fro object
            // if slot active select item
            // call event assigned
        }

        private void OnItemAdded(Transform item)
        {
            // if active slot is empty AssignItemToSlot()
        }
    }
}
