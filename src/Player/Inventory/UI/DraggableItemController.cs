using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace U3.Player.Inventory.UI
{
    public class DraggableItemController : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Transform[] itemsParents; // 0 == initial; 1 == slot;
        private RectTransform rTransform;
        private Canvas canvas;
        private DraggableItemMaster draggableMaster;

        private void Awake()
        {
            rTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();

            itemsParents = new Transform[2];
            itemsParents[0] = rTransform.parent;

            draggableMaster = GetComponent<DraggableItemMaster>();
        }
        private void OnEnable()
        {
            draggableMaster.EventOriginChanged += SetSlotParent;
        }
        private void OnDisable()
        {
            draggableMaster.EventOriginChanged -= SetSlotParent;
            RestoreParent();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            rTransform.SetParent(canvas.transform);
            draggableMaster.CallEventDragBegin(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            rTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            draggableMaster.CallEventOnDrag(eventData);
        }

        private void RestoreParent()
        {
            int idx = itemsParents[1] == null ? 0 : 1; // if element was not assigned to slot move back to origin, move back to slot otherwise

            rTransform.SetParent(itemsParents[idx]);
        }

        private void FreePreviousContainer()
        {
            if (itemsParents[1] == null)
                return;

            if (itemsParents[1].TryGetComponent(out DraggableContainer container))
                container.ClearItemUI();
        }

        private void TryChangeParent(PointerEventData eventData)
        {
            List<RaycastResult> results = new();

            EventSystem.current.RaycastAll(eventData, results);

            foreach (RaycastResult res in results)
            {
                if (res.gameObject.TryGetComponent(out DraggableContainer container))
                {
                    if (container.ItemUI != null)
                        continue;

                    FreePreviousContainer();
                    container.SetItemUI(rTransform);
                    break;
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            TryChangeParent(eventData);
            RestoreParent();
            draggableMaster.CallEventDragEnd(eventData);
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            
        }

        private void SetSlotParent(Transform slotParent)
        {
            itemsParents[1] = slotParent;
        }
    }
}
