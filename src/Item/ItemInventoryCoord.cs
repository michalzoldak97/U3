using UnityEngine;

namespace U3.Item
{
    public class ItemPositioner : MonoBehaviour
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
            transform.SetLocalPositionAndRotation(
                itemMaster.ItemSettings.OnParentPosition,
                Quaternion.Euler(itemMaster.ItemSettings.OnParentRotation));
        }
    }
}
