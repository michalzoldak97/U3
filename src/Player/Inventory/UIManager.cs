using UnityEngine;

namespace U3.Player.Inventory
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryUI; // parent object

        [SerializeField] private Transform slotsParent, itemsParent; // ui object containers

        [SerializeField] private GameObject slotLabelPrefab, containerPrefab, itemPrefab; // ui elements prefabs

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
            LoadSlotsUI();
        }

        private void Start()
        {
            LoadUI();
        }
    }
}
