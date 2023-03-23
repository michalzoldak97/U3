using System.Collections.Generic;
using U3.Inventory;
using U3.Player.Inventory.UI;
using UnityEngine;

namespace U3.Player.Inventory
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryUI; // parent object

        [SerializeField] private Transform slotsParent, itemsParent; // ui object containers

        [SerializeField] private GameObject slotLabelPrefab, containerPrefab; // ui elements prefabs

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

        private void LoadItemsUI()
        {
            foreach (KeyValuePair<Transform, InventoryItem> item in inventoryMaster.Items)
            {
                if (item.Value.IsAssignedToSlot)
                    continue;

                GameObject itemButton = Instantiate(item.Value.ItemMaster.ItemSettings.UIPrefab, itemsParent);

                UI.Item itemUIObj = itemButton.GetComponent<UI.Item>();
                itemUIObj.SetItemName(item.Value.ItemMaster.ItemSettings.ToItemName);
                itemUIObj.SetItemIcon(item.Value.ItemMaster.ItemSettings.ItemIcon);
            }
        }
        private void LoadSlotsUI()
        {
            foreach (Slot slot in inventoryMaster.Slots)
            {
                GameObject slotLabel = Instantiate(slotLabelPrefab, slotsParent);
                slotLabel.GetComponent<UI.Slot>().SetSlotName(slot.Name);

                for (int i = 0; i < slot.Containers.Length; i++)
                {
                    Instantiate(containerPrefab, slotsParent);
                }
            }
        }
        private void LoadUI()
        {
            LoadItemsUI();
            LoadSlotsUI();
        }

        private void Start()
        {
            LoadUI();
        }
    }
}
