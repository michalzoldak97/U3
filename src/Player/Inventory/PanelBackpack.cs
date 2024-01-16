using TMPro;
using U3.Inventory;
using U3.Log;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class PanelBackpack : MonoBehaviour, IInventoryDropArea
    {
        [SerializeField] private Transform itemsParent;
        [SerializeField] private TMP_Text headerCounter;

        private PlayerInventoryMaster inventoryMaster;

        private void ListBackpackItems()
        {

        }

        private void UpdateHeader()
        {

        }

        private void SetInit()
        {
            if (transform.root.TryGetComponent(out PlayerInventoryMaster playerInventoryMaster))
            {
                inventoryMaster = playerInventoryMaster;
            }
            else
            {
                GameLogger.Log(new GameLog(
                Log.LogType.Error,
                    $"There is no PlayerInventoryMaster on the {name} root"));
            }
        }

        private void OnEnable()
        {
            if (inventoryMaster == null)
                SetInit();

            ListBackpackItems();
            UpdateHeader();
        }

        private void OnDisable()
        {
            inventoryMaster.PlayerMaster.UpdateInventorySettings();
        }

        public bool OnInventoryItemDrop(InventoryItem item)
        {
            // if there is a capacity add new to the list
            return false;
        }
    }
}
