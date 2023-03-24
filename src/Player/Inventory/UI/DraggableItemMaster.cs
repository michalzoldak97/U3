using UnityEngine;
using UnityEngine.EventSystems;

namespace U3.Player.Inventory.UI
{
    public class DraggableItemMaster : MonoBehaviour
    {
        public delegate void DraggableEventHandler(PointerEventData eventData);

        public event DraggableEventHandler EventDragBegin;
        public event DraggableEventHandler EventOnDrag;
        public event DraggableEventHandler EventDragEnd;

        public delegate void DraggableInteractionEventHandler(Transform t);

        public event DraggableInteractionEventHandler EventOriginChanged;

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

        public void CallEventOriginChanged(Transform t)
        {
            EventOriginChanged?.Invoke(t);
        }
    }
}
