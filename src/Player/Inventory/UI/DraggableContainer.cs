using U3.Player.Inventory.UI;
using UnityEngine;

namespace U3
{
    public class DraggableContainer : MonoBehaviour, IDropContainer
    {
        public RectTransform ItemUI { get; private set; }

        public bool IsDropAvailable()
        {
            return ItemUI == null;
        }
        public void OnDropAttempt(RectTransform t)
        {
            if (ItemUI != null)
                return;

            if (t.TryGetComponent(out DraggableItemMaster dMaster))
            {
                dMaster.CallEventSlotOriginChanged(transform);
                ItemUI = t;
            }
        }

        public void ClearItemUI()
        {
            ItemUI = null;
        }
    }
}
