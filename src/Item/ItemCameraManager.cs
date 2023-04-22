using U3.Inventory;
using UnityEngine;

namespace U3.Item
{
    public class ItemCameraManager : MonoBehaviour
    {
       private bool shouldInformCamera;
       private ItemMaster itemMaster;
       private IInventoryMaster inventoryMaster;

       private void OnEnable()
       {
            itemMaster = GetComponent<ItemMaster>();

            itemMaster.EventSelected += OnSelected;
            itemMaster.EventDeselected += OnDeselected;
            // TODO: handle toggle call
       }

        private void OnDisable()
       {
            itemMaster.EventSelected -= OnSelected;
            itemMaster.EventDeselected -= OnDeselected;
       }

       private void OnSelected()
       {
            if (inventoryMaster == null)
                inventoryMaster = transform.root.GetComponent<IInventoryMaster>();
            
            shouldInformCamera = true;
       }

       private void OnDeselected()
       {
            shouldInformCamera = false;
            inventoryMaster = null;
       }

        private void ToggleCamera(bool toActiveState)
        {
            if (inventoryMaster == null ||
                inventoryMaster.ItemCamera == null)
                return;

            inventoryMaster.ItemCamera.SetActive(toActiveState);
            shouldInformCamera = !toActiveState;
        }

       private void OnTriggerStay(Collider other)
       {
            if (shouldInformCamera)
                ToggleCamera(true);
       }

       private void OnTriggerExit(Collider other)
       {
            ToggleCamera(false);
       }
    }
}
