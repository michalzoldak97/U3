using U3.Player.UI;
using UnityEngine;

namespace U3.Player.Inventory.UI
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

        public void Disable()
        {
            foreach (IPlayerUIScreenStateDependent pd in GetComponentsInChildren<IPlayerUIScreenStateDependent>())
            {
                pd.CallEventUIScreenDisabled();
            }

            inventoryMaster.CallEventInventoryScreenClosed();
        }

        public void Enable()
        {
            inventoryMaster.CallEventInventoryScreenOpened();
        }
    }
}