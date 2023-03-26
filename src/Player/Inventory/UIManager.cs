using System.Collections.Generic;
using U3.Inventory;
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

            inventoryMaster.EventItemAdded += ReloadUI;
            inventoryMaster.EventItemRemoved += ReloadUI;
            inventoryMaster.EventItemAddedToContainer += ReloadUI;
            inventoryMaster.EventItemRemovedFromContainer += ReloadUI;
            inventoryMaster.EventInventoryUIReloadRequest += ReloadUI;
        }
        private void OnDisable()
        {
            inventoryMaster.EventItemAdded -= ReloadUI;
            inventoryMaster.EventItemRemoved -= ReloadUI;
            inventoryMaster.EventItemAddedToContainer -= ReloadUI;
            inventoryMaster.EventItemRemovedFromContainer -= ReloadUI;
            inventoryMaster.EventInventoryUIReloadRequest -= ReloadUI;
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
                itemUIObj.InventoryItem = item.Key;
            }
        }
        /// <summary>
        /// Reloads slot ui from ground up
        /// Instantiantes item button on the container if item is assigned
        /// </summary>
        private void LoadSlotsUI()
        {
            for (int i = 0; i < inventoryMaster.Slots.Length; i++)
            {
                GameObject slotLabel = Instantiate(slotLabelPrefab, slotsParent);
                slotLabel.GetComponent<UI.Slot>().SetSlotName(inventoryMaster.Slots[i].Name);

                for (int j = 0; j < inventoryMaster.Slots[i].Containers.Length; j++)
                {
                    GameObject cObj = Instantiate(containerPrefab, slotsParent);

                    UI.Container container = cObj.GetComponent<UI.Container>();
                    container.ContainerIDX[0] = i; container.ContainerIDX[1] = j;
                    container.InventoryMaster = inventoryMaster;

                    Transform assignedItem = inventoryMaster.Slots[i].Containers[j].Item; // if container has assigned item

                    if (assignedItem == null)
                        continue;

                    InventoryItem item = inventoryMaster.Items[assignedItem];
                    if (item == null)
                        continue;

                    GameObject itemButton = Instantiate(item.ItemMaster.ItemSettings.UIPrefab, container.transform);

                    if (cObj.TryGetComponent(out UI.DraggableContainer draggableContainer))
                    {
                        draggableContainer.ItemUI = itemButton.GetComponent<RectTransform>();
                    }
                    else
                    {
                        Log.GameLogger.Log(Log.LogType.Error, "trying to spawn item object on the container without draggable component");
                        continue;
                    }

                    UI.Item itemUIObj = itemButton.GetComponent<UI.Item>();
                    itemUIObj.SetItemName(item.ItemMaster.ItemSettings.ToItemName);
                    itemUIObj.SetItemIcon(item.ItemMaster.ItemSettings.ItemIcon);
                    itemUIObj.SetUIParent(cObj.transform);
                    itemUIObj.InventoryItem = assignedItem;
                }
            }
        }
        private void LoadUI()
        {
            LoadItemsUI();
            LoadSlotsUI();
        }
        private void ReloadUI()
        {
            foreach (Transform t in slotsParent)
            {
                Destroy(t.gameObject);
            }

            foreach (Transform t in itemsParent)
            {
                Destroy(t.gameObject);
            }

            LoadUI();
        }
        private void Start()
        {
            LoadUI();
        }
    }
}
