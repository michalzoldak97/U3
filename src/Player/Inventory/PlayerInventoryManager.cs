using U3.Inventory;
using UnityEngine;

namespace U3.Player.Inventory
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

        private void OnItemAdded(Transform item)
        {
            // if active slot is empty AssignItemToSlot()
        }

        private void OnSlotSelected(Transform item)
        {

        }
    }
}
