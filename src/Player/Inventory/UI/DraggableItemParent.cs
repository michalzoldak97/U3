using UnityEngine;

namespace U3.Player.Inventory.UI
{
    public class DraggableItemParent : MonoBehaviour, IDropContainer
    {
        public bool IsDropAvailable()
        {
            return true;
        }

        public void OnDropAttempt(RectTransform t)
        {
            if (t.TryGetComponent(out DraggableItemMaster dMaster))
            {
                dMaster.CallEventSlotOriginChanged(null);
            }
        }
    }
}
