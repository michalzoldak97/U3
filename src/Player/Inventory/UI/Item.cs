using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace U3.Player.Inventory.UI
{
    public class Item: MonoBehaviour
    {
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private Image itemIcon;

        public Transform InventoryItem { get; set; }

        public void SetItemName (string name)
        {
            itemName.text = name;
        }

        public void SetItemIcon(Sprite icon)
        {
            itemIcon.sprite = icon;
        }

        public void SetUIParent(Transform toSet)
        {
            if (gameObject.TryGetComponent(out DraggableItemMaster iMaster))
            {
                iMaster.CallEventSlotOriginChanged(toSet);
            }
        }
    }
}
