using UnityEngine;

namespace U3.Item
{
    public class ItemName : MonoBehaviour
    {
        private void Start()
        {
            if (TryGetComponent(out ItemMaster itemMaster))
            {
                if (itemMaster.ItemSettings.ToItemName != "")
                    gameObject.name = itemMaster.ItemSettings.ToItemName;
            }
        }
    }
}
