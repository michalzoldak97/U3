using U3.Input;
using U3.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

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

            ActionMapManager.PlayerInputActions.Humanoid.ItemThrow.performed += OnItemThrow;
            ActionMapManager.PlayerInputActions.Humanoid.ItemThrow.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            ActionMapManager.PlayerInputActions.Humanoid.ItemThrow.performed -= OnItemThrow;
            ActionMapManager.PlayerInputActions.Humanoid.ItemThrow.Disable();
        }

        private void OnItemThrow(InputAction.CallbackContext obj)
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
