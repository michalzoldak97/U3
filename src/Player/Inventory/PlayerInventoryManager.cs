using U3.Input;
using U3.Inventory;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class PlayerInventoryManager : InventoryManager
    {
        [SerializeField] private ItemSlotParent[] SlotParents;

        private PlayerInventoryMaster playerInventoryMaster;

        protected override void SetInit()
        {
            base.SetInit();
            playerInventoryMaster = GetComponent<PlayerInventoryMaster>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            PlayerInputManager.UIInputActions.EventItemThrow += OnItemThrow;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PlayerInputManager.UIInputActions.EventItemThrow -= OnItemThrow;
        }

        private void OnItemThrow()
        {
            if (playerInventoryMaster.FocusedItem == null)
                return;

            InventoryItem toThrow = inventoryMaster.Items.GetItem(playerInventoryMaster.FocusedItem);
            if (toThrow == null)
                return;

            playerInventoryMaster.CallEventRemoveItem(toThrow.Item);
        }

        private void Start()
        {
            InventorySlotsInitializer.Initialize(playerInventoryMaster, SlotParents);
            BackpackInitializer.Initialize(playerInventoryMaster);
        }
    }
}
