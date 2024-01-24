using UnityEngine;

namespace U3.Player.Inventory
{
    public class ItemAssignmentManager : MonoBehaviour
    {
        private PlayerInventoryMaster inventoryMaster;

        private void SetInit()
        {
            inventoryMaster = GetComponent<PlayerInventoryMaster>();
        }

        private void OnEnable()
        {
            SetInit();
        }

        private void OnDisable()
        {
            
        }
    }
}
