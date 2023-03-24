using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace U3.Player.Inventory.UI
{
    public class Draggable : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool FollowCursor { get; set; } = true;
        public bool CanBeDragged { get; set; } = true;
        public Vector3 StartPosition { get; set; }

        public event Action<PointerEventData> OnBeginDragHandler;
        public event Action<PointerEventData> OnDragHandler;
        public event Action<PointerEventData, bool> OnEndDragHandler;

        private Transform uiParent;
        private RectTransform rTransform;
        private Canvas canvas;

        private void Awake()
        {
            rTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            uiParent = rTransform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!CanBeDragged)
                return;

            rTransform.SetParent(canvas.transform);
            OnBeginDragHandler?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!CanBeDragged)
                return;

            OnDragHandler?.Invoke(eventData);

            if (!FollowCursor)
                return;

            rTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            rTransform.parent = uiParent;
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
        }
    }
}
