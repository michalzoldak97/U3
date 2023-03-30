using UnityEngine;

namespace U3.Item
{
    public class ItemInventoryCoord : MonoBehaviour
    {
        private ItemMaster itemMaster;

        private void OnEnable()
        {
            itemMaster = GetComponent<ItemMaster>();

            itemMaster.EventAddedToInventory += SetPositionAndRotation;
        }

        private void OnDisable()
        {
            itemMaster.EventAddedToInventory -= SetPositionAndRotation;
        }

        private void SetPositionAndRotation()
        {

            gameObject.transform.localPosition = itemMaster.ItemSettings.OnParentPosition;

            gameObject.transform.localRotation = Quaternion.Euler(itemMaster.ItemSettings.OnParentRotation);
        }
    }
}
