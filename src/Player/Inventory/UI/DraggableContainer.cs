using UnityEngine;

namespace U3.Player.Inventory.UI
{
    public class DraggableContainer : MonoBehaviour, IDropContainer
    {
        public RectTransform ItemUI { get; set; }

        private Container container;

        private void OnEnable()
        {
            container = GetComponent<Container>();
        }

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
            else
            {
                Log.GameLogger.Log(Log.LogType.Error, "dropped invenory item obj without draggable item master");
                return;
            }    

            if (t.TryGetComponent(out Item item))
            {
                container.ItemAdded(item.InventoryItem);
            }
            else
            {
                Log.GameLogger.Log(Log.LogType.Error, "dropped invenory item obj without item component, slot event will not be invoked");
                return;
            }
        }

        public void ClearItemUI()
        {
            if (ItemUI.TryGetComponent(out Item item))
            {
                container.ItemRemoved(item.InventoryItem);
            }
            else
            {
                Log.GameLogger.Log(Log.LogType.Error, "removed invenory item obj without item component, slot event will not be invoked");
                return;
            }

            ItemUI = null;
        }
    }
}
