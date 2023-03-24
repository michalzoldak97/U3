using U3.Player.Inventory.UI;
using UnityEngine;

namespace U3
{
    public class DraggableContainer : MonoBehaviour
    {
        public RectTransform ItemUI { get; private set; }

        public void SetItemUI(RectTransform t)
        {
            if (ItemUI != null)
                return;

            if (t.TryGetComponent(out DraggableItemMaster dMaster))
            {
                dMaster.CallEventOriginChanged(transform);
                ItemUI = t;
            }
        }

        public void ClearItemUI()
        {
            ItemUI = null;
        }
    }
}
