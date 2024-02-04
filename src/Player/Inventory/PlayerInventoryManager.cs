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
            playerInventoryMaster.EventSlotSelected += OnSlotSelected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            playerInventoryMaster.EventSlotSelected += OnSlotSelected;
        }

        private void OnSlotSelected(Transform item)
        {

        }

        private void Start()
        {
            InventorySlotsInitializer.Initialize(playerInventoryMaster, SlotParents);
            BackpackInitializer.Initialize(playerInventoryMaster);
        }
    }
}
