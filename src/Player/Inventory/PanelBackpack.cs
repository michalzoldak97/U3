using System.Linq;
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

        public Transform AreaTransform => itemsParent;

        private PlayerInventoryMaster inventoryMaster;

        private void ListBackpackItems()
        {
            foreach (InventoryItem inventoryItem in inventoryMaster.GetBackpackItems())
            {
                ItemButtonFactory.AddItemButton(inventoryItem, inventoryMaster, this);
            }
        }

        private void UpdateHeader()
        {
            headerCounter.text = $"{inventoryMaster.GetBackpackItems().Count()} | " +
                $"{inventoryMaster.PlayerMaster.PlayerSettings.Inventory.BackpackCapacity}";
        }

        private void UpdateBackpack()
        {
            foreach (Transform itemButton in itemsParent)
            {
                Destroy(itemButton.gameObject);
            }

            ListBackpackItems();
            UpdateHeader();
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

            UpdateBackpack();
        }

        private void OnEnable()
        {
            SetInit();

            inventoryMaster.EventInventoryCleared += OnEventToUpdateBackpack;

            inventoryMaster.EventItemAdded += OnEventToUpdateBackpack;
            inventoryMaster.EventItemSelected += OnEventToUpdateBackpack;
            inventoryMaster.EventItemDeselected += OnEventToUpdateBackpack;
            inventoryMaster.EventItemRemoved += OnEventToUpdateBackpack;

            inventoryMaster.EventOnItemButtonDrop += OnEventToUpdateBackpack;

            inventoryMaster.EventReloadBackpack += OnEventToUpdateBackpack;
        }

        private void OnDisable()
        {
            inventoryMaster.EventInventoryCleared -= OnEventToUpdateBackpack;

            inventoryMaster.EventItemAdded -= OnEventToUpdateBackpack;
            inventoryMaster.EventItemSelected -= OnEventToUpdateBackpack;
            inventoryMaster.EventItemDeselected -= OnEventToUpdateBackpack;
            inventoryMaster.EventItemRemoved -= OnEventToUpdateBackpack;

            inventoryMaster.EventOnItemButtonDrop -= OnEventToUpdateBackpack;

            inventoryMaster.EventReloadBackpack -= OnEventToUpdateBackpack;

            inventoryMaster.PlayerMaster.UpdateInventorySettings();
        }

        private void OnEventToUpdateBackpack() => UpdateBackpack();
        private void OnEventToUpdateBackpack(Transform item) => UpdateBackpack();
        private void OnEventToUpdateBackpack(IItemButton itemButton, IInventoryDropArea dropArea) => UpdateBackpack();

        public bool OnInventoryItemDrop(InventoryItem item)
        {
            // if there is a capacity add new to the list
            return false;
        }
    }
}
