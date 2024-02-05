using UnityEngine;
using U3.Player.Inventory;

namespace U3.Player.UI
{
    public class InventoryUIScreen : MonoBehaviour, IUIScreen
    {
        public UIScreenType UIScreenType { get; private set; } = UIScreenType.Inventory;
        public GameObject ScreenObj { get; set; }

        private PlayerInventoryMaster inventoryMaster;

        private void Awake()
        {
            inventoryMaster = GetComponentInParent<PlayerInventoryMaster>();
        }

        private void OnDisable()
        {
            inventoryMaster.CallEventItemUnfocused(inventoryMaster.FocusedItem);
        }
    }
}