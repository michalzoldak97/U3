using U3.Player.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace U3.Player.Inventory.UI
{
    public class DraggableItemMaster : MonoBehaviour, IPlayerUIScreenStateDependent
    {
        public delegate void DraggableEventHandler(PointerEventData eventData);

        public event DraggableEventHandler EventDragBegin;
        public event DraggableEventHandler EventOnDrag;
        public event DraggableEventHandler EventDragEnd;

        public delegate void DraggableInteractionEventHandler(Transform t);

        public event DraggableInteractionEventHandler EventSlotOriginChanged;

        public delegate void DraggableScreenParentEventHandler();

        public event DraggableScreenParentEventHandler EventUIScreenDisabled;

        public void CallEventDragBegin(PointerEventData eventData)
        {
            EventDragBegin?.Invoke(eventData);
        }
        public void CallEventOnDrag(PointerEventData eventData)
        {
            EventOnDrag?.Invoke(eventData);
        }
        public void CallEventDragEnd(PointerEventData eventData)
        {
            EventDragEnd?.Invoke(eventData);
        }

        public void CallEventSlotOriginChanged(Transform t)
        {
            EventSlotOriginChanged?.Invoke(t);
        }

        public void CallEventUIScreenDisabled()
        {
            EventUIScreenDisabled?.Invoke();
        }
    }
}
